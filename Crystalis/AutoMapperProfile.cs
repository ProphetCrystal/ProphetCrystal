using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.Models;
using Crystalis.Models.Campaign;

namespace Crystalis;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCampaignDto, Campaign>();
        CreateMap<UpdateCampaignDto, Campaign>();
        CreateMap<Campaign, CampaignDto>();
    }
}