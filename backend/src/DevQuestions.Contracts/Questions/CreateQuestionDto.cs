namespace DevQuestions.Contracts.Questions;

public record CreateQuestionDto(string Title, string Description, Guid UserId, Guid[] TagIds);