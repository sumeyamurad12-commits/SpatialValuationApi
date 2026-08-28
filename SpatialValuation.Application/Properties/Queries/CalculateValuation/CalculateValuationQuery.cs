using MediatR;

namespace SpatialValuation.Application.Properties.Queries.CalculateValuation;

public record CalculateValuationQuery(
    double Longitude,
    double Latitude,
    double SizeInSquareMeters,
    string PropertyType,
    double SearchRadiusMeters = 2000) : IRequest<ValuationResultDto>;