using MediatR;
using Microsoft.EntityFrameworkCore;
using SpatialValuation.Application.Common.Interfaces;
using SpatialValuation.Application.Properties.DTOs;

namespace SpatialValuation.Application.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto?>
{
    private readonly IValuationDbContext _context;

    public GetPropertyByIdQueryHandler(IValuationDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (property is null)
        {
            return null;
        }

        return new PropertyDto
        {
            Id = property.Id,
            ParcelNumber = property.ParcelNumber ?? string.Empty,
            PropertyType = property.PropertyType.ToString(),
            ZoningType = property.ZoningType.ToString(),
            SizeInSquareMeters = property.SizeInSquareMeters,
            Longitude = property.Location.X,
            Latitude = property.Location.Y,
            SubCity = property.Address.SubCity ?? string.Empty,
            Woreda = property.Address.Woreda ?? string.Empty,
            HouseNumber = property.Address.HouseNumber ?? string.Empty,
            StreetName = property.Address.StreetName ?? string.Empty,
            City = property.Address.City
        };
    }
}