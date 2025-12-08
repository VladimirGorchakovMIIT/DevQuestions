using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionService
{
    /// <summary>
    ///Создание вопроса 
    /// </summary>
    /// <param name="questionDto">CreateQuestionDto - dto для создания вопроса</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Guid ID созданного вопроса</returns>
    Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление ответа на вопрос
    /// </summary>
    /// <param name="questionId">Guid ID вопроса</param>
    /// <param name="addAnswerDto">DTO для добавления ответа на вопрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>При успешном добавлении нового ответа возвращает Guid ID, а при неуспешном возвращает ошибку, либо массив ошибок</returns>
    Task<Result<Guid, Failure>> AddAnswer(Guid questionId, AddAnswerDto request, CancellationToken cancellationToken);
}