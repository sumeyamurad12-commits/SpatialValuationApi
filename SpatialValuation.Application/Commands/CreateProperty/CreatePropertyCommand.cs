using MediatR;
using SpatialValuation.Domain.Enums;

namespace SpatialValuation.Application.Properties.Commands.CreateProperty;

public record CreatePropertyCommand(
    string ParcelNumber,
    PropertyType PropertyType,
    ZoningType ZoningType,
    double SizeInSquareMeters,
    double Latitude,
    double Longitude,
    string City,
    string SubCity,
    string Woreda,
    string? HouseNumber,
    string? StreetName
) : IRequest<Guid>;