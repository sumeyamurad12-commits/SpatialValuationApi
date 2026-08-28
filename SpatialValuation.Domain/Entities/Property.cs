using SpatialValuation.Domain.Enums;
using SpatialValuation.Domain.ValueObjects;
using NetTopologySuite.Geometries;

namespace SpatialValuation.Domain.Entities;

public class Property
{
    public Guid Id { get; private set; }
    public string ParcelNumber { get; private set; } = string.Empty;
    public PropertyType PropertyType { get; private set; }
    public ZoningType ZoningType { get; private set; }
    public double SizeInSquareMeters { get; private set; }
    public Point Location { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    // Page 2 Physical Building Attributes
    public double BuildingFootprintSquareMeters { get; set; }
    public int NumberOfStories { get; set; } = 1;
    public int YearBuilt { get; set; }
    public FinishGrade FinishGrade { get; set; } = FinishGrade.Standard;
    private Property() { } // EF Core Private Constructor

    public Property(string parcelNumber, PropertyType propertyType, ZoningType zoningType, double sizeInSquareMeters, Point location, Address address)
    {
        Id = Guid.NewGuid();
        ParcelNumber = parcelNumber;
        PropertyType = propertyType;
        ZoningType = zoningType;
        SizeInSquareMeters = sizeInSquareMeters;
        Location = location;
        Address = address;
        CreatedAt = DateTime.UtcNow;
    }

    // Static Factory Method
    public static Property Create(string parcelNumber, PropertyType propertyType, ZoningType zoningType, double sizeInSquareMeters, Point location, Address address)
    {
        return new Property(parcelNumber, propertyType, zoningType, sizeInSquareMeters, location, address);
    }
}
