namespace SpatialValuation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
{
    // Fixes schema ID collisions for classes with the same name in different namespaces
    c.CustomSchemaIds(type => type.FullName);
});

        // Future API machines go here (CORS, Auth, Exception Filters, Rate Limiting)

        return services;
    }
}