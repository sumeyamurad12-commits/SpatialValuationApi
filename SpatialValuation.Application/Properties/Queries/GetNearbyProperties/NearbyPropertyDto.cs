namespace SpatialValuation.Application.Properties.Queries.GetNearbyProperties;

public class NearbyPropertyDto
{
    public Guid Id { get; set; }
    public string ParcelNumber { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public string ZoningType { get; set; } = string.Empty;
    public double SizeInSquareMeters { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double DistanceInMeters { get; set; }
    public string SubCity { get; set; } = string.Empty;
    public string Woreda { get; set; } = string.Empty;
}