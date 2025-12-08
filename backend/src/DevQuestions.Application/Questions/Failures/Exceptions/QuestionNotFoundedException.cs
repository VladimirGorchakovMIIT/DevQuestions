using DevQuestions.Application.Exceptions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures.Exceptions;

public class QuestionNotFoundedException(Error[] errors) : NotFoundException(errors);