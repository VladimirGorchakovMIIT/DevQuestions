namespace DevQuestions.Contracts;

public record UpdateQuestionDto(string Title, string Description, Guid[] TagIds);