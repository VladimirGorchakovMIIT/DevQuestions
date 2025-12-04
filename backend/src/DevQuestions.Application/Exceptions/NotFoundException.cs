using System.Text.Json;
using DevQuestions.Shared;

namespace DevQuestions.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Error[] errors) : base(JsonSerializer.Serialize(errors))
    {
         
    }
}