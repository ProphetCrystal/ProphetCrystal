using FluentValidation;
using PaymentsService.Validators.Extensions;
using ProphetCrystal.DTO.Campaign;

namespace ProphetCrystal.Validators.Validators.Campaign;

public class GetCampaignDtoValidator : AbstractValidator<GetCampaignDto>
{
    public GetCampaignDtoValidator (IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Uuid).NotEmpty().Exists("Campaigns", "Uuid", serviceProvider);
    }
}