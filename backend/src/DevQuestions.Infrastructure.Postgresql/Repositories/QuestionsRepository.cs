using CSharpFunctionalExtensions;
using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Failures;
using DevQuestions.Application.Questions.GetQuestionsWithFilters;
using DevQuestions.Domain.Questions;
using DevQuestions.Shared;
using Microsoft.EntityFrameworkCore;
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
        _dbContext.Questions.Attach(question);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return question.Id;
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);
        if(question is null)
            return Errors.General.RecordNotFounded(questionId).ToFailure();
        
        return question;
    }

    public Task<(IReadOnlyList<Question> Questions, long Count)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetOpenUserQuestionAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}