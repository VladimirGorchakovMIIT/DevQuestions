using CSharpFunctionalExtensions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Communication;

public interface IUsersCommunicationService
{
    Task<Result<long, Failure>> GetUserRating(Guid userId, CancellationToken cancellationToken = default);
}