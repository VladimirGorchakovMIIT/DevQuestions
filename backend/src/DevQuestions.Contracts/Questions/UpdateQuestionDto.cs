namespace DevQuestions.Contracts.Questions;

public record UpdateQuestionDto(string Title, string Description, Guid[] TagIds);