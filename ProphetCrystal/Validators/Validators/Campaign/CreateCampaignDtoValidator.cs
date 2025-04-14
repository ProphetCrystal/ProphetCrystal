using FluentValidation;
using PaymentsService.Validators.Extensions;
using ProphetCrystal.DTO.Campaign;

namespace ProphetCrystal.Validators.Validators.Campaign;

public class CreateCampaignDtoValidator : AbstractValidator<CreateCampaignDto>
{
    public CreateCampaignDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}