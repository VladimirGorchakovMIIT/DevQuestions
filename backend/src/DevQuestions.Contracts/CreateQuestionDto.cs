namespace DevQuestions.Contracts;

public record CreateQuestionDto(string Title, string Description, Guid UserId, Guid[] TagIds);