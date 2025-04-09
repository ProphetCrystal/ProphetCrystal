using Crystalis.DTO.Campaign;
using Crystalis.DTO.World;
using FluentValidation;
using PaymentsService.Validators.Extensions;

namespace Crystalis.Validators.Validators.World;

public class GetWorldDtoValidator : AbstractValidator<GetWorldDto>
{
    public GetWorldDtoValidator (IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Uuid).NotEmpty().Exists("Campaigns", "Uuid", serviceProvider);
    }
}