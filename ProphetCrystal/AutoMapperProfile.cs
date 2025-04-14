using AutoMapper;
using ProphetCrystal.DTO.Campaign;
using ProphetCrystal.DTO.Note;
using ProphetCrystal.DTO.World;
using ProphetCrystal.Models;

namespace ProphetCrystal;

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