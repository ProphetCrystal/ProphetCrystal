using Crystalis.DTO.Campaign;
using FluentValidation;
using PaymentsService.Validators.Extensions;

namespace Crystalis.Validators.Validators.Campaign;

public class GetCampaignDtoValidator : AbstractValidator<GetCampaignDto>
{
    public GetCampaignDtoValidator (IServiceProvider serviceProvider)
    {
        RuleFor(x => x.Uuid).NotEmpty().Exists("Campaigns", "Uuid", serviceProvider);
    }
}