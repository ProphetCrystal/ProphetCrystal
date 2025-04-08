using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.Models;

namespace Crystalis;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCampaignDto, Campaign>();
        CreateMap<Campaign, CampaignDto>();
    }
}