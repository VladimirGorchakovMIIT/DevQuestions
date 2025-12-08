using DevQuestions.Application.Exceptions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures.Exceptions;

public class QuestionValidationException(Error[] errors) : BadRequestException(errors);