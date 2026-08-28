using SpatialValuation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpatialValuation.Application.Common.Interfaces;

namespace SpatialValuation.Infrastructure.Persistence;

public class ValuationDbContext : DbContext, IValuationDbContext
{
    public ValuationDbContext(DbContextOptions<ValuationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties => Set<Property>();
    public DbSet<ComparableSale> ComparableSales => Set<ComparableSale>();

    public DbSet<ComparableSale> PropertySales => Set<ComparableSale>();
    public DbSet<Road> Roads => Set<Road>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Property entity (existing)
        builder.Entity<Property>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Location).HasColumnType("geometry(Point, 4326)");
            entity.HasIndex(p => p.Location).HasMethod("GIST"); // Spatial Index
        });

        // 2. Configure Road entity & PostGIS Spatial Index
        builder.Entity<Road>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name)
          .IsRequired(false); // Allows NULL in PostgreSQL database column

            // Map LineString geometry with SRID 4326 (WGS 84)
            entity.Property(r => r.Geometry)
                  .HasColumnType("geometry(Geometry, 4326)")
                  .IsRequired();

            // Create PostGIS GIST spatial index on Geometry for fast ST_Distance queries
            entity.HasIndex(r => r.Geometry)
                  .HasMethod("GIST");
        });

        // Enables PostGIS spatial extension in PostgreSQL
        builder.HasPostgresExtension("postgis");

        // Automatically applies all entity configurations from current assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ValuationDbContext).Assembly);
    }
}
