using ProphetCrystal.DTO.Campaign;
using FluentValidation;
using PaymentsService.Validators.Extensions;
using ProphetCrystal.DTO.World;

namespace ProphetCrystal.Validators.Validators.World;

public class CreateWorldDtoValidator : AbstractValidator<CreateWorldDto>
{
    public CreateWorldDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}