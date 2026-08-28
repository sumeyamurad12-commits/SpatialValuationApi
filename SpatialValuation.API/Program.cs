using SpatialValuation.Infrastructure;
using SpatialValuation.Application;
using SpatialValuation.Api;
using SpatialValuation.Api.Middleware;
using SpatialValuation.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure Services (PostGIS DbContext)
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
// 1. Register DbContextInitialiser in DI
builder.Services.AddScoped<DbContextInitialiser>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails(); // Generates default problem details metadata
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Register global exception handler
var app = builder.Build();

// Execute Seeder with explicit logging
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Starting database seeding...");
        var initialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();
        await initialiser.SeedAsync();
        logger.LogInformation("Database seeding completed successfully!");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
app.UseExceptionHandler(); // Enable global exception handling pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 
