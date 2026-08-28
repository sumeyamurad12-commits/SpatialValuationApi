namespace SpatialValuation.Application.Properties.DTOs;

public class PropertyDto
{
    public Guid Id { get; set; }
    public string ParcelNumber { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public string ZoningType { get; set; } = string.Empty;
    public double SizeInSquareMeters { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    // Full Address Value Object Mapping
    public string SubCity { get; set; } = string.Empty;
    public string Woreda { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}