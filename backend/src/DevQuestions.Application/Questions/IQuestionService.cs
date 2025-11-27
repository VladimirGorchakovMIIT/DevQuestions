using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionService
{
    Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken);
}