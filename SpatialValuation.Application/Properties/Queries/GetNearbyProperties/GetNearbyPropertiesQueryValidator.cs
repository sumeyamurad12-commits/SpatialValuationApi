using FluentValidation;

namespace SpatialValuation.Application.Properties.Queries.GetNearbyProperties;

public class GetNearbyPropertiesQueryValidator : AbstractValidator<GetNearbyPropertiesQuery>
{
    public GetNearbyPropertiesQueryValidator()
    {
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180.0, 180.0)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90.0, 90.0)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.DistanceInMeters)
            .GreaterThan(0)
            .WithMessage("Search distance must be greater than 0 meters.")
            .LessThanOrEqualTo(50000)
            .WithMessage("Search distance cannot exceed 50,000 meters (50 km).");
    }
}