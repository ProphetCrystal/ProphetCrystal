using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.DTO.Note;
using Crystalis.DTO.World;
using Crystalis.Models;

namespace Crystalis;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCampaignDto, Campaign>();
        CreateMap<UpdateCampaignDto, Campaign>();
        CreateMap<Campaign, CampaignDto>();

        CreateMap<CreateWorldDto, World>();
        CreateMap<UpdateWorldDto, World>();
        CreateMap<World, WorldDto>();

        CreateMap<Note, NoteDto>();
    }
}