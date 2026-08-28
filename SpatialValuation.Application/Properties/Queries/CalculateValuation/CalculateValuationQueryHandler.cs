using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SpatialValuation.Application.Common.Interfaces;

namespace SpatialValuation.Application.Properties.Queries.CalculateValuation;

public class CalculateValuationQueryHandler
    : IRequestHandler<CalculateValuationQuery, ValuationResultDto>
{
    private readonly IValuationDbContext _context;

    public CalculateValuationQueryHandler(IValuationDbContext context)
    {
        _context = context;
    }

    public async Task<ValuationResultDto> Handle(
        CalculateValuationQuery request,
        CancellationToken cancellationToken)
    {
        var targetPoint = new Point(request.Longitude, request.Latitude) { SRID = 4326 };

        // 1. Retrieve nearby spatial samples using PostGIS ST_DWithin
        var nearbyProperties = await _context.Properties
            .AsNoTracking()
            .Where(p => p.Location.IsWithinDistance(targetPoint, request.SearchRadiusMeters))
            .Select(p => new
            {
                p.SizeInSquareMeters,
                p.PropertyType,
                Distance = p.Location.Distance(targetPoint)
            })
            .ToListAsync(cancellationToken);

        // 2. Base fallback if no spatial samples exist in range
        if (!nearbyProperties.Any())
        {
            decimal baseFallbackRate = 45000m; // Default ETB/m2 base rate
            return new ValuationResultDto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                SizeInSquareMeters = request.SizeInSquareMeters,
                PropertyType = request.PropertyType,
                SampleCountUsed = 0,
                EstimatedPricePerSquareMeter = baseFallbackRate,
                TotalEstimatedValue = (decimal)request.SizeInSquareMeters * baseFallbackRate,
                ValuationConfidence = "Low (No Nearby PostGIS Samples)"
            };
        }

        // 3. Perform Inverse Distance Weighting (IDW) calculation
        double totalWeight = 0;
        double weightedRateSum = 0;
        decimal baseRate = 50000m; // Reference market rate per m2

        foreach (var sample in nearbyProperties)
        {
            double distance = Math.Max(sample.Distance, 1.0); // Avoid division by zero
            double weight = 1.0 / distance; // Inverse distance weighting

            totalWeight += weight;
            weightedRateSum += (double)baseRate * weight;
        }

        decimal finalRatePerSqM = (decimal)(weightedRateSum / totalWeight);
        decimal totalValuation = finalRatePerSqM * (decimal)request.SizeInSquareMeters;

        string confidence = nearbyProperties.Count >= 5 ? "High" : "Medium";

        return new ValuationResultDto
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            SizeInSquareMeters = request.SizeInSquareMeters,
            PropertyType = request.PropertyType,
            SampleCountUsed = nearbyProperties.Count,
            EstimatedPricePerSquareMeter = Math.Round(finalRatePerSqM, 2),
            TotalEstimatedValue = Math.Round(totalValuation, 2),
            ValuationConfidence = confidence
        };
    }
}