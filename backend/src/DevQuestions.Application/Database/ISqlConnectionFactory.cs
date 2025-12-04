using System.Data;

namespace DevQuestions.Application.Database;

public interface ISqlConnectionFactory
{
    public IDbConnection Create();
}