using DevQuestions.Application;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgrammeDependencies(this IServiceCollection services)
    {
        AddWebDependencies(services);
        
        services.AddApplication();

        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        
        return services;
    }
}