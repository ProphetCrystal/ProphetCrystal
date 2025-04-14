using FluentValidation;
using PaymentsService.Validators.Extensions;
using ProphetCrystal.DTO.Campaign;

namespace ProphetCrystal.Validators.Validators.Campaign;

public class UpdateCampaignDtoValidator : AbstractValidator<UpdateCampaignDto>
{
    public UpdateCampaignDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}