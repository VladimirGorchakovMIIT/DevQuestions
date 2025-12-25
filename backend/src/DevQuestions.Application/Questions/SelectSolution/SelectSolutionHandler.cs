using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions.Abstractions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions.SelectSolution;

public class SelectSolutionHandler : IHandler<Guid, SelectSolutionCommand>
{
    public Task<Result<Guid, Failure>> Handle(SelectSolutionCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}