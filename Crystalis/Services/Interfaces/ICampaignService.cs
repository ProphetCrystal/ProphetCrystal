using System.Security.Claims;
using Crystalis.DTO.Campaign;
using Crystalis.Enums;
using Crystalis.Models;
using Sieve.Models;

namespace Crystalis.Services.Interfaces;

public interface ICampaignService
{
    public Task<List<GetCampaignDto>> Get(SieveModel sieveModel, ClaimsPrincipal user);
    public GetCampaignDto Get(int id);
    public GetCampaignDto Get(string id);
    public Task<GetCampaignDto> Add(CreateCampaignDto campaign, ClaimsPrincipal user);
    public GetCampaignDto Update(Campaign campaign);
    public void Delete(int id);
    public void Delete(string id);
    public Task<GetCampaignDto> Join(JoinCampaignDto joinCampaignDto, ClaimsPrincipal user);
    public Task<bool> Leave(LeaveCampaignDto joinCampaignDto, ClaimsPrincipal user);
}