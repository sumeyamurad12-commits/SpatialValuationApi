namespace SpatialValuation.Application.Valuations.Queries.CalculatePropertyValuation;

public class ValuationResultDto
{
    public double TargetLongitude { get; set; }
    public double TargetLatitude { get; set; }
    public double PropertySizeInSquareMeters { get; set; }
    public double BuildingFootprintSquareMeters { get; set; }
    public int NumberOfStories { get; set; }
    public int BuildingAgeYears { get; set; }
    public string FinishGrade { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public string ZoningType { get; set; } = string.Empty;
    public int ComparablePropertiesCount { get; set; }
    public double AveragePricePerSquareMeter { get; set; }
    public double EstimatedMarketValue { get; set; }
    public double SearchRadiusInMeters { get; set; }
    public double AccuracyPercentage { get; set; }
    public string? ValuationNotice { get; set; }
    public double DistanceToRoadMeters { get; set; }
    public string? NearestRoadName { get; set; }
    public TransactionTaxResultDto TransactionTaxBreakdown { get; set; } = null!;

}