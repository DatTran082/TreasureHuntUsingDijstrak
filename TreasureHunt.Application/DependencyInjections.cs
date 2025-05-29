
using Microsoft.Extensions.DependencyInjection;
using TreasureHunt.Application.Interfaces;

namespace TreasureHunt.Application.Services;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITreasureSolverService, TreasureSolverService>();
        return services;
    }


}