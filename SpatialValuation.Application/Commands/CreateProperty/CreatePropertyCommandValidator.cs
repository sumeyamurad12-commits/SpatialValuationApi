using FluentValidation;

namespace SpatialValuation.Application.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.ParcelNumber)
            .NotEmpty().WithMessage("Parcel number is required.")
            .MaximumLength(50);

        RuleFor(x => x.SizeInSquareMeters)
            .GreaterThan(0).WithMessage("Property size must be greater than 0.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Valid latitude is required.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Valid longitude is required.");

        RuleFor(x => x.SubCity).NotEmpty();
        RuleFor(x => x.Woreda).NotEmpty();
    }
}