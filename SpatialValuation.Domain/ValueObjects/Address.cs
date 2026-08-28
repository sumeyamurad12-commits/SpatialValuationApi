namespace SpatialValuation.Domain.ValueObjects;

public record Address(
    string SubCity,
    string Woreda,
    string? HouseNumber,
    string? StreetName,
    string City = "Addis Ababa"
);
