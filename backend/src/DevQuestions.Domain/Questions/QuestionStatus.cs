namespace DevQuestions.Domain.Questions;

public enum QuestionStatus
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    Open,

    /// <summary>
    /// Статус проверен
    /// </summary>
    Resolved,
}

public static class QuestionStatusExtensions
{
    public static string ToConvertRussianLanguage(this QuestionStatus status) => status switch
    {
        QuestionStatus.Open => "Открыт",
        QuestionStatus.Resolved => "Решен",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };
}