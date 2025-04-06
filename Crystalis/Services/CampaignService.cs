using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services.Interfaces;

namespace Crystalis.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignService;
    private readonly IMapper _mapper;

    public CampaignService(ICampaignRepository campaignRepository, IMapper mapper)
    {
        _mapper = mapper;
        _campaignService = campaignRepository;
    }

    public List<GetCampaignDto> Get()
    {
        throw new NotImplementedException();
    }

    public GetCampaignDto Get(int id)
    {
        throw new NotImplementedException();
    }

    public GetCampaignDto Get(string id)
    {
        throw new NotImplementedException();
    }

    public GetCampaignDto Add(CreateCampaignDto campaign)
    {
        var createdCampaign = _campaignService.Add(_mapper.Map<Campaign>(campaign));
        return _mapper.Map<GetCampaignDto>(createdCampaign);
    }

    public GetCampaignDto Update(Campaign campaign)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}