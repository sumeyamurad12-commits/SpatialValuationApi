namespace SpatialValuation.Application.Properties.Queries.CalculateValuation;

public class ValuationResultDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double SizeInSquareMeters { get; set; }
    public string PropertyType { get; set; } = string.Empty;
    public int SampleCountUsed { get; set; }
    public decimal EstimatedPricePerSquareMeter { get; set; }
    public decimal TotalEstimatedValue { get; set; }
    public string ValuationConfidence { get; set; } = string.Empty;
}