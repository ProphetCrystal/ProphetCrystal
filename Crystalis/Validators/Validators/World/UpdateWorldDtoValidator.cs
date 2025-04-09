using Crystalis.DTO.Campaign;
using Crystalis.DTO.World;
using FluentValidation;
using PaymentsService.Validators.Extensions;

namespace Crystalis.Validators.Validators.World;

public class UpdateWorldDtoValidator : AbstractValidator<UpdateWorldDto>
{
    public UpdateWorldDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}