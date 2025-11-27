namespace DevQuestions.Contracts.Questions;

public record GetQuestionRequestDto(string Search, Guid[] TagIds, int Page, int PageSize);