namespace DevQuestions.Contracts.Questions.Dtos;

public record GetQuestionRequestDto(string Search, Guid[] TagIds, int Page, int PageSize);