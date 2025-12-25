namespace DevQuestions.Contracts.Questions.Dtos;

public record QuestionsDto(
    Guid Id,
    string Title,
    string Text, 
    Guid UserId, 
    string? ScreenshotUrl, 
    Guid? SolutionId,
    IEnumerable<string> Tags,
    string Status);