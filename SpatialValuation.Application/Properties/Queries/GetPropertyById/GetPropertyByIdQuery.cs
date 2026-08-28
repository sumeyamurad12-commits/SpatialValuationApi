using MediatR;
using SpatialValuation.Application.Properties.DTOs;

namespace SpatialValuation.Application.Properties.Queries.GetPropertyById;

public record GetPropertyByIdQuery(Guid Id) : IRequest<PropertyDto?>;