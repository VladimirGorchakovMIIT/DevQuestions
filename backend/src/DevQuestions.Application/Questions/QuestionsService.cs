using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Application.Questions.Failures.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionService
{
    private readonly IQuestionsRepository _repository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository repository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToErrors();
            throw new QuestionValidationException(errors);
        }
        
        // валидация бизнес логики
        var openUserQuestionCount = await _repository.GetOpenUserQuestionAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionCount > 3)
            throw new TooManyQuestionsException([Errors.Questions.TooManyQuestions()]);
        
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
    // public async Task AddAnswer(
    //     Guid questionId,
    //     AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
}