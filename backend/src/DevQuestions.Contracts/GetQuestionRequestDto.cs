namespace DevQuestions.Contracts;

public record GetQuestionRequestDto(string Search, Guid[] TagIds, int Page, int PageSize);