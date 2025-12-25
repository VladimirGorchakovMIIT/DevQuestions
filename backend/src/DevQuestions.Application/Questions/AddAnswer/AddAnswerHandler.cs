using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerHandler : IHandler<Guid, AddAnswerCommand>
{
    private readonly IValidator<AddAnswerDto> _validator;
    private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ITransactionManager _transactionManager;
    private readonly IQuestionsRepository _repository;
    private readonly ILogger<QuestionsService> _logger;

    public AddAnswerHandler(IValidator<AddAnswerDto> validator, IUsersCommunicationService usersCommunicationService, ILogger<QuestionsService> logger, ITransactionManager transactionManager, IQuestionsRepository repository)
    {
        _validator = validator;
        _usersCommunicationService = usersCommunicationService;
        _logger = logger;
        _transactionManager = transactionManager;
        _repository = repository;
    }

    public async Task<Result<Guid, Failure>> Handle(AddAnswerCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var userRatingResult = await _usersCommunicationService.GetUserRating(command.AddAnswerDto.UserId, cancellationToken);
        if (userRatingResult.Value <= 0)
            return Errors.Questions.NotEnoughRating();

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _repository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;
        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, questionResult.Value);

        question.Answers.Add(answer);
        await _repository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Added answer with id: {AnswerId} to question {questionId}", answer.Id, command.QuestionId);

        return answer.Id;
    }
}