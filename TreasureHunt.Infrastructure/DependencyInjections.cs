namespace TreasureHunt.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TreasureHunt.Infrastructure.Context;
using TreasureHunt.Infrastructure.Interfaces;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastStructure(this IServiceCollection services, IConfiguration config)
    {
        //     services.AddDbContext<TreasureDbContext>(options =>
        //    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddDbContext<TreasureDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            )
        );

        services.AddEndpointsApiExplorer();
        services.AddScoped<ITreasureMapService, TreasureMapService>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TreasureHunt API", Version = "v1" });
        });

        return services;
    }
}