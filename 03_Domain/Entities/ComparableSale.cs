using NetTopologySuite.Geometries;

namespace _03_Domain.Entities;

public class ComparableSale
{
    // Industry standard: 3 years (36 months) max age for spatial market comparisons
    public const int DefaultMaxComparableAgeYears = 3;

    public Guid Id { get; private set; }
    public Guid PropertyId { get; private set; }
    public decimal SalePrice { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Point Location { get; private set; } = null!;

    private ComparableSale() { } // EF Core Private Constructor

    public ComparableSale(Guid propertyId, decimal salePrice, DateTime saleDate, Point location)
    {
        if (saleDate > DateTime.UtcNow)
            throw new ArgumentException("Sale date cannot be in the future.", nameof(saleDate));

        Id = Guid.NewGuid();
        PropertyId = propertyId;
        SalePrice = salePrice;
        SaleDate = saleDate;
        Location = location;
    }

    /// <summary>
    /// Domain Rule: Evaluates if this sale occurred within the acceptable valuation window.
    /// </summary>
    public bool IsValidComparable(DateTime referenceDate, int maxYears = DefaultMaxComparableAgeYears)
    {
        var thresholdDate = referenceDate.AddYears(-maxYears);
        return SaleDate >= thresholdDate && SaleDate <= referenceDate;
    }
}