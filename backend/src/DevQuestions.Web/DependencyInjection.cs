using DevQuestions.Application;
using DevQuestions.Application.DependencyInjection;
using DevQuestions.Infrastructure.Communication.DependencyInjection;
using DevQuestions.Infrastructure.DependencyInjection;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgrammeDependencies(this IServiceCollection services)
    {
        AddWebDependencies(services);
        
        services.AddApplication();
        services.AddPostgresqlInfrastructure();
        services.AddInfrastructureCommunication();

        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        
        return services;
    }
}