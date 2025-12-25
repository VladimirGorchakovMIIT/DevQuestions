using DevQuestions.Application.Questions.Abstractions;

namespace DevQuestions.Application.Questions.GetQuestionsWithFilters;

public record GetQuestionsWithFiltersCommand(int PageNumber, int PageSize, string Search, IEnumerable<Guid> Tags) : ICommand;