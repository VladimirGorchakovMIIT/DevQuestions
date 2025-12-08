using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures;

public partial class Errors
{
    public static class General
    {
        public static Error RecordNotFounded(Guid id) => Error.NotFound("record.not.founded", $"Запись по id {id} в базе данных не найдена", id);
    }

    public static class Questions
    {
        public static Error TooManyQuestions() => Error.Failure("questions.too.many", "Пользователь не может открыть больше 3 вопросов");

        public static Failure NotEnoughRating() => Error.Failure("not.enough.rating", "Недостаточно рейтинга");
    }
}