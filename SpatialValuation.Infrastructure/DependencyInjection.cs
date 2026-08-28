using SpatialValuation.Infrastructure.Persistence;
using SpatialValuation.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpatialValuation.Infrastructure.Services;

namespace SpatialValuation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ValuationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // Enable PostGIS spatial mapping in EF Core
                npgsqlOptions.UseNetTopologySuite();
                npgsqlOptions.MigrationsAssembly(typeof(ValuationDbContext).Assembly.FullName);
            }));
        // This line connects the Application layer interface to the Infrastructure DbContext
        services.AddScoped<IValuationDbContext>(provider => provider.GetRequiredService<ValuationDbContext>());
        services.AddTransient<ITransactionTaxCalculator, TransactionTaxCalculator>();
        return services;
        
    }
}

