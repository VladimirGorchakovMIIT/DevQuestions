namespace DevQuestions.Contracts.Questions.Dtos;

public record CreateQuestionDto(string Title, string Description, Guid UserId, Guid[] TagIds);