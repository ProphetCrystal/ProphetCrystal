using FluentValidation;
using PaymentsService.Validators.Extensions;
using ProphetCrystal.DTO.World;

namespace ProphetCrystal.Validators.Validators.World;

public class GetWorldDtoValidator : AbstractValidator<GetWorldDto>
{
    public GetWorldDtoValidator(IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Uuid).NotEmpty().Exists("Worlds", "Uuid", serviceProvider);
    }
}