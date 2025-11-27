using System.Collections.ObjectModel;
using DevQuestions.Domain.Reports;
using DevQuestions.Domain.Tags;

namespace DevQuestions.Domain.Questions;

public class Question
{
    public Question(
        Guid id,
        string title,
        string description,
        Guid userId,
        Guid? screenShotId,
        IEnumerable<Guid> tags)
    {
        Id = id;
        Title = title;
        Description = description;
        UserId = userId;
        ScreenShotId = screenShotId;
        Tags = tags.ToList();
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid UserId { get; set; }

    public Guid? ScreenShotId { get; set; }

    public List<Answer> Answers { get; set; } = [];

    public Answer? Solution { get; set; }

    public List<Guid> Tags { get; set; } = [];

    public QuestionStatus QuestionStatus { get; set; } = QuestionStatus.Open;
}