using MediatR;

namespace SpatialValuation.Application.Properties.Queries.GetNearbyProperties;

public record GetNearbyPropertiesQuery(
    double Longitude,
    double Latitude,
    double DistanceInMeters = 1000) : IRequest<List<NearbyPropertyDto>>;