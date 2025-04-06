using Crystalis.DTO.Campaign;
using Crystalis.Enums;
using Crystalis.Models;

namespace Crystalis.Services.Interfaces;

public interface ICampaignService
{
    public List<GetCampaignDto> Get();
    public GetCampaignDto Get(int id);
    public GetCampaignDto Get(string id);
    public GetCampaignDto Add(CreateCampaignDto campaign);
    public GetCampaignDto Update(Campaign campaign);
    public void Delete(int id);
    public void Delete(string id);
}