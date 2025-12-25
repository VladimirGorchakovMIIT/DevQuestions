using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions.GetQuestionsWithFilters;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);
    
    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);
    
    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);

    Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Question> Questions, long Count)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken);
    
    Task<int> GetOpenUserQuestionAsync(Guid userId, CancellationToken cancellationToken);
}