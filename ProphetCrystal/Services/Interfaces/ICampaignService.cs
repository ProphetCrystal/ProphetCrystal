using System.Security.Claims;
using ProphetCrystal.Enums;
using ProphetCrystal.Models;
using ProphetCrystal.DTO.Campaign;
using Sieve.Models;

namespace ProphetCrystal.Services.Interfaces;

public interface ICampaignService
{
    public Task<List<CampaignDto>> Get(SieveModel sieveModel, ClaimsPrincipal user);
    public CampaignDto Get(int id);
    public CampaignDto Get(string id);
    public Task<CampaignDto> Create(CreateCampaignDto campaign, ClaimsPrincipal user);
    public CampaignDto Update(UpdateCampaignDto campaign);
    public void Delete(int id);
    public void Delete(string id);
    public Task<CampaignDto> Join(GetCampaignDto getCampaignDto, ClaimsPrincipal user);
    public Task<bool> Leave(GetCampaignDto getCampaignDto, ClaimsPrincipal user);
}