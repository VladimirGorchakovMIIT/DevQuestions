using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures;

public partial class Errors
{
    public static class Questions
    {
        public static Error TooManyQuestions() => Error.Failure("questions.too.many", "Пользователь не может открыть больше 3 вопросов");
    }
} 