using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DevQuestions.Infrastructure.Repositories;

public class QuestionsRepository : IQuestionsRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    
    public QuestionsRepository(ApplicationDbContext dbContext, IConfiguration configuration, ISqlConnectionFactory sqlConnectionFactory)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = $"""
                            insert into questions (id, title, text, user_id, screenshot_id, tags, status)
                            values (@Id, @Title, @Text, @UserId, @ScreenshotId, @Tags, @Status)
                            """;

        using var connection = _sqlConnectionFactory.Create();

        await connection.ExecuteAsync(sql, new
        {
            Id = question.Id,
            Title = question.Title,
            Text = question.Description,
            UserId = question.UserId,
            ScreenShotId = question.ScreenShotId,
            Tags = question.Tags.ToArray(),
            QuestionStatus = question.QuestionStatus,
        });
        
        return question.Id;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Question> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetOpenUserQuestionAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}