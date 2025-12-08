namespace DevQuestions.Domain.Questions;

public class Answer
{
    public Answer(Guid id, Guid userId, string text, Question question)
    {
        Id = id;
        UserId = userId;
        Text = text;
        Question = question;
        Rating = 0;
    }

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Text { get; set; }

    public Question Question { get; set; }

    public List<Guid> Comments { get; set; } = [];

    public double Rating { get; set; }
}