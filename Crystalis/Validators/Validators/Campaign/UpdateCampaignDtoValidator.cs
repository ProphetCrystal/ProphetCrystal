using Crystalis.DTO.Campaign;
using FluentValidation;
using PaymentsService.Validators.Extensions;

namespace Crystalis.Validators.Validators.Campaign;

public class UpdateCampaignDtoValidator : AbstractValidator<UpdateCampaignDto>
{
    public UpdateCampaignDtoValidator ()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}