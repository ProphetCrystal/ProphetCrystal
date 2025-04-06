using System.Security.Claims;
using Crystalis.DTO.Campaign;
using Crystalis.Enums;
using Crystalis.Models;

namespace Crystalis.Services.Interfaces;

public interface ICampaignService
{
    public List<GetCampaignDto> Get();
    public GetCampaignDto Get(int id);
    public GetCampaignDto Get(string id);
    public Task<GetCampaignDto> Add(CreateCampaignDto campaign, ClaimsPrincipal user);
    public GetCampaignDto Update(Campaign campaign);
    public void Delete(int id);
    public void Delete(string id);
}