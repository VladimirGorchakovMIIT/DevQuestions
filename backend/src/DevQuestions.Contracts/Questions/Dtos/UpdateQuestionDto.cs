namespace DevQuestions.Contracts.Questions.Dtos;

public record UpdateQuestionDto(string Title, string Description, Guid[] TagIds);