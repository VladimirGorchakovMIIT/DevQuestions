using DevQuestions.Application.Exceptions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.Failures.Exceptions;

public class QuestionNotFoundedException : NotFoundException
{
    public QuestionNotFoundedException(Error[] errors) : base(errors)
    {
        
    }
}