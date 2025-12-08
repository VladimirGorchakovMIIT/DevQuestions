using CSharpFunctionalExtensions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions.CreateQuestions;

public class CreateQuestionHandler : ICommandHandler<Guid, CreateQuestionCommand>
{
    private readonly IQuestionsRepository _repository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public CreateQuestionHandler(
        IValidator<CreateQuestionDto> validator,
        IQuestionsRepository repository,
        ILogger<QuestionsService> logger
    )
    {
        _logger = logger;
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result<Guid, Failure>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _validator.ValidateAsync(command.QuestionDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        // валидация бизнес логики
        var openUserQuestionCount = await _repository.GetOpenUserQuestionAsync(command.QuestionDto.UserId, cancellationToken);
        if (openUserQuestionCount > 3)
            return Errors.Questions.TooManyQuestions().ToFailure();

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            command.QuestionDto.Title,
            command.QuestionDto.Description,
            command.QuestionDto.UserId,
            null,
            command.QuestionDto.TagIds.ToList());

        await _repository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Created question with id: {QuestionId}", questionId);

        return questionId;
    }
}