using MediatR;
using NetTopologySuite.Geometries;
using SpatialValuation.Application.Common.Interfaces;
using SpatialValuation.Domain.Entities;
using SpatialValuation.Domain.ValueObjects;

namespace SpatialValuation.Application.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
{
    private readonly IValuationDbContext _context;
    private readonly GeometryFactory _geometryFactory;

    public CreatePropertyCommandHandler(IValuationDbContext context)
    {
        _context = context;
        // WGS 84 spatial reference (SRID 4326)
        _geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
    }

    public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        // NetTopologySuite uses (Longitude/X, Latitude/Y) order for spatial coordinates
        var locationPoint = _geometryFactory.CreatePoint(new Coordinate(request.Longitude, request.Latitude));

        var address = new Address(
            request.SubCity,
            request.Woreda,
            request.HouseNumber,
            request.StreetName,
            request.City
        );

        var property = new Property(
            request.ParcelNumber,
            request.PropertyType,
            request.ZoningType,
            request.SizeInSquareMeters,
            locationPoint,
            address
        );

        _context.Properties.Add(property);
        await _context.SaveChangesAsync(cancellationToken);

        return property.Id;
    }
}