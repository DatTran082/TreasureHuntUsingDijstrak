
using TreasureHunt.Application;
using TreasureHunt.Application.Services;
using TreasureHunt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastStructure(builder.Configuration)
        .AddCors(options =>
    {
        options.AddPolicy("AllowLocalAndVercel",
            policy => policy
                .WithOrigins("http://localhost:3000", "https://trandashboard.vercel.app")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
    });
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseCors("AllowLocalAndVercel");
    app.UseHttpsRedirection();
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TreasureHunt API v1");
    });

    app.Run();
}