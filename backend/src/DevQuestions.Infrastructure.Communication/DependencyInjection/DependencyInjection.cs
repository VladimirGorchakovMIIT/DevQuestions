using DevQuestions.Application.Communication;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.Communication.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureCommunication(this IServiceCollection services)
    {
        services.AddScoped<IUsersCommunicationService, UsersCommunicationService>();
        
        return services;
    }
}