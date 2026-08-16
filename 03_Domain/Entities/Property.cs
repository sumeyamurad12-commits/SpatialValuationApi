using _03_Domain.Enums;
using _03_Domain.ValueObjects;
using NetTopologySuite.Geometries;

namespace _03_Domain.Entities;

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
}