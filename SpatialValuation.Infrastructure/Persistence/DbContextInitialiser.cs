using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SpatialValuation.Domain.Entities;
using SpatialValuation.Domain.Enums;
using NetTopologySuite;
using SpatialValuation.Domain.ValueObjects;

// Explicit type alias forces C# to resolve to your Domain Entity
using SpatialProperty = SpatialValuation.Domain.Entities.Property;

namespace SpatialValuation.Infrastructure.Persistence;

public class DbContextInitialiser
{
    private readonly ValuationDbContext _context;
    private static readonly GeometryFactory GeometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public DbContextInitialiser(ValuationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.Properties.AnyAsync())

        {
            var sampleProperties = new List<SpatialProperty>
            {
            new Property(
                "AA-ARADA-00100",
                PropertyType.Residential,
                ZoningType.ResidentialLowDensity,
                450.0,
                GeometryFactory.CreatePoint(new Coordinate(38.7578, 9.0320)),
                new Address("Arada", "Woreda 01", "101", "Churchill Road", "Addis Ababa")
            ),
            new Property(
                "AA-KIRKOS-00200",
                PropertyType.Commercial,
                ZoningType.CommercialZone,
                800.0,
                GeometryFactory.CreatePoint(new Coordinate(38.7612, 9.0150)),
                new Address("Kirkos", "Woreda 08", "202", "Meskel Square St", "Addis Ababa")
            ),
            new Property(
                "AA-BOLE-00300",
                PropertyType.MixedUse,
                ZoningType.CommercialZone,
                1200.0,
                GeometryFactory.CreatePoint(new Coordinate(38.7889, 8.9950)),
                new Address("Bole", "Woreda 03", "303", "Bole Road", "Addis Ababa")
            ),
            new Property(
                "AA-KOLFE-00400",
                PropertyType.Residential,
                ZoningType.ResidentialHighDensity,
                300.0,
                GeometryFactory.CreatePoint(new Coordinate(38.7120, 9.0280)),
                new Address("Kolfe Keranyo", "Woreda 04", "404", "Ring Road", "Addis Ababa")
            )
            
        };

            await _context.Properties.AddRangeAsync(sampleProperties);
            await _context.SaveChangesAsync();
        }
    }
}