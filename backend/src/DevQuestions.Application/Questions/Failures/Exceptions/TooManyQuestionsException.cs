using DevQuestions.Application.Exceptions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures.Exceptions;

public class TooManyQuestionsException : BadRequestException
{
    public TooManyQuestionsException(Error[] errors) : base(errors)
    {
        
    }
}