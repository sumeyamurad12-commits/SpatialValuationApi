using Microsoft.EntityFrameworkCore;
using SpatialValuation.Domain.Entities;

namespace SpatialValuation.Application.Common.Interfaces;

public interface IValuationDbContext
{
    DbSet<Property> Properties { get; }
    DbSet<ComparableSale> PropertySales { get; }
    DbSet<Road> Roads { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}