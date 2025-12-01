using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresqlInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IQuestionsRepository, QuestionsRepository>();
        
        return services;
    }
}