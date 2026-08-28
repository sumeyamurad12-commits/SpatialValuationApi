using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpatialValuation.Domain.Entities;

namespace SpatialValuation.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("properties");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Parcel Number (Unique Business Key)
        builder.Property(p => p.ParcelNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.ParcelNumber)
            .IsUnique();

        // Physical Dimensions
        builder.Property(p => p.SizeInSquareMeters)
            .IsRequired();

        // PostGIS Spatial Point Column (SRID 4326 = WGS 84 GPS Standard)
        builder.Property(p => p.Location)
            .HasColumnType("geometry(Point, 4326)")
            .IsRequired();

        // PostGIS GiST Spatial Index for Fast ST_DWithin / Buffer Queries
        builder.HasIndex(p => p.Location)
            .HasMethod("gist");

        // Map Address Value Object as Owned Entity (Flattens columns into 'properties' table)
        builder.OwnsOne(p => p.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.SubCity)
                .HasColumnName("sub_city")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Woreda)
                .HasColumnName("woreda")
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(a => a.HouseNumber)
                .HasColumnName("house_number")
                .HasMaxLength(50);

            addressBuilder.Property(a => a.StreetName)
                .HasColumnName("street_name")
                .HasMaxLength(150);

            addressBuilder.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();
        });
    }
}