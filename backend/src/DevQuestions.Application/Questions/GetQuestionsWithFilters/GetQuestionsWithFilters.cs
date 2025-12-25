using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IHandler<QuestionResponse, GetQuestionsWithFiltersCommand>
{
    private readonly IQuestionsRepository _questionRepository;
    private readonly ITagsRepository _tagsRepository;

    public GetQuestionsWithFilters(IQuestionsRepository questionRepository, ITagsRepository tagsRepository)
    {
        _questionRepository = questionRepository;
        _tagsRepository = tagsRepository;
    }

    public async Task<Result<QuestionResponse, Failure>> Handle(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetQuestionsWithFiltersAsync(command, cancellationToken);

        var questionTags = questions.SelectMany(q => q.Tags);
        var tags = await _tagsRepository.GetTagsAsync(questionTags, cancellationToken);
        
        var questionsDto = questions.Select(x => new QuestionsDto(
            x.Id, x.Title, x.Description, x.UserId, x.ScreenShotId.ToString(), x.Solution?.Id, tags, x.QuestionStatus.ToConvertRussianLanguage()));
    }
}