using SpatialValuation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpatialValuation.Infrastructure.Persistence.Configurations;

public class ComparableSaleConfiguration : IEntityTypeConfiguration<ComparableSale>
{
    public void Configure(EntityTypeBuilder<ComparableSale> builder)
    {
        builder.ToTable("comparable_sales");

        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.PropertyId)
            .IsRequired();

        builder.Property(cs => cs.SalePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(cs => cs.SaleDate)
            .IsRequired();

        // Spatial location of sale
        builder.Property(cs => cs.Location)
            .HasColumnType("geometry(Point, 4326)")
            .IsRequired();

        // Spatial index for PostGIS distance queries
        builder.HasIndex(cs => cs.Location)
            .HasMethod("gist");

        // B-Tree index on SaleDate for fast rolling 3-year date filtering
        builder.HasIndex(cs => cs.SaleDate);
    }
}
