using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SpatialValuation.Application.Common.Interfaces;
using SpatialValuation.Domain.Entities;

namespace SpatialValuation.Application.Properties.Queries.GetNearbyProperties;

public class GetNearbyPropertiesQueryHandler
    : IRequestHandler<GetNearbyPropertiesQuery, List<NearbyPropertyDto>>
{
    private readonly IValuationDbContext _context;

    public GetNearbyPropertiesQueryHandler(IValuationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NearbyPropertyDto>> Handle(
        GetNearbyPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        // 1. Construct NetTopologySuite Point for target coordinates (SRID 4326 for WGS 84)
        var searchPoint = new Point(request.Longitude, request.Latitude) { SRID = 4326 };

        // 2. Convert search radius from meters to spatial degrees (~1 degree = 111,320 meters in WGS 84)
        double radiusInDegrees = request.DistanceInMeters / 111320.0;

        // 2. Query PostGIS with spatial proximity filtering
        var properties = await _context.Properties
            .AsNoTracking()
            .Where(p => p.Location.IsWithinDistance(searchPoint, radiusInDegrees))
            .ToListAsync(cancellationToken);

        return properties
         .Select(p => new NearbyPropertyDto
            {
                Id = p.Id,
                ParcelNumber = p.ParcelNumber,
                PropertyType = p.PropertyType.ToString(),
                ZoningType = p.ZoningType.ToString(),
                SizeInSquareMeters = p.SizeInSquareMeters,
                Longitude = p.Location.X,
                Latitude = p.Location.Y,
                // Convert degree distance to meters
                DistanceInMeters = Math.Round(p.Location.Distance(searchPoint) * 111320.0, 2),
                SubCity = p.Address.SubCity,
                Woreda = p.Address.Woreda
            })
            .OrderBy(p => p.DistanceInMeters)
            .ToList();
    }
}