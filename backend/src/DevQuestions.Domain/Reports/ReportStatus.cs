namespace DevQuestions.Domain.Reports;

public enum ReportStatus
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    Open,
    /// <summary>
    /// Статус проверен
    /// </summary>
    Resolved,
    /// <summary>
    /// Статут отклонен
    /// </summary>
    Dismissed,
    /// <summary>
    /// Статус в процессе рассмотрения
    /// </summary>
    InProgress
}