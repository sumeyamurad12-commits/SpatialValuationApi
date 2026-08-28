using FluentValidation;

namespace SpatialValuation.Application.Properties.Queries.CalculateValuation;

public class CalculateValuationQueryValidator : AbstractValidator<CalculateValuationQuery>
{
    public CalculateValuationQueryValidator()
    {
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180.0, 180.0)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90.0, 90.0)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.SizeInSquareMeters)
            .GreaterThan(0)
            .WithMessage("Property size must be greater than 0 square meters.");

        RuleFor(x => x.SearchRadiusMeters)
            .InclusiveBetween(50.0, 50000.0)
            .WithMessage("Search radius must be between 50 and 50,000 meters.");

        RuleFor(x => x.PropertyType)
            .NotEmpty()
            .WithMessage("Property type is required.");
    }
}