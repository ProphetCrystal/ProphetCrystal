using Crystalis.DTO.Campaign;
using Crystalis.DTO.World;
using FluentValidation;
using PaymentsService.Validators.Extensions;

namespace Crystalis.Validators.Validators.World;

public class CreateWorldDtoValidator : AbstractValidator<CreateWorldDto>
{
    public CreateWorldDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}