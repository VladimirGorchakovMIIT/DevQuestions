using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Application.Questions.Failures.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Result = CSharpFunctionalExtensions.Result;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionService
{
    private readonly IQuestionsRepository _repository;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtoValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly ITransactionManager _transactionManager;
    private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(
        IQuestionsRepository repository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> createQuestionDtoValidator, IValidator<AddAnswerDto> addAnswerDtoValidator, ITransactionManager transactionManager, IUsersCommunicationService usersCommunicationService)
    {
        _repository = repository;
        _logger = logger;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _transactionManager = transactionManager;
        _usersCommunicationService = usersCommunicationService;
    }

    public async Task<Result<Guid, Failure>> Handler(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _createQuestionDtoValidator.ValidateAsync(questionDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        // валидация бизнес логики
        var openUserQuestionCount = await _repository.GetOpenUserQuestionAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionCount > 3)
            return Errors.Questions.TooManyQuestions().ToFailure();

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Description,
            questionDto.UserId,
            null,
            questionDto.TagIds.ToList());

        await _repository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Created question with id: {QuestionId}", questionId);

        return questionId;
    }

    // public async Task Get(GetQuestionRequestDto question, CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task GetById(Guid questionId)
    // {
    // }
    //
    // public async Task Put(
    //     Guid questionId,
    //     UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task Delete(Guid questionId, CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task SelectSolution(
    //     Guid questionId,
    //     Guid correctAnswerId,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    public async Task<Result<Guid, Failure>> AddAnswer(Guid questionId, AddAnswerDto addAnswerDto, CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(addAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var userRatingResult = await _usersCommunicationService.GetUserRating(addAnswerDto.UserId, cancellationToken);
        if (userRatingResult.Value <= 0)
            return Errors.Questions.NotEnoughRating();

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _repository.GetByIdAsync(questionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;
        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionResult.Value);

        question.Answers.Add(answer);
        await _repository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Added answer with id: {AnswerId} to question {questionId}", answer.Id, questionId);

        return answer.Id;
    }
}