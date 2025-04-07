using System.Security.Claims;
using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.Enums;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sieve.Models;

namespace Crystalis.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public CampaignService(ICampaignRepository campaignRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _campaignRepository = campaignRepository;
    }

    public async Task<List<GetCampaignDto>> Get(SieveModel sieveModel, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<List<GetCampaignDto>>(_campaignRepository.Get(sieveModel, user.IsInRole("Admin"), currentUser.Id));
    }

    public GetCampaignDto Get(int id)
    {
        throw new NotImplementedException();
    }

    public GetCampaignDto Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<GetCampaignDto> Add(CreateCampaignDto campaign, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        var mappedCampaign = _mapper.Map<Campaign>(campaign);
        mappedCampaign.Author = currentUser;
        mappedCampaign.CampaignUsers.Add( new()
        {
            UserId = currentUser.Id,
            Role = CampaignUserRole.GameMaster
        });
        var createdCampaign = _campaignRepository.Add(mappedCampaign);
        
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

    public async Task<GetCampaignDto> Join(JoinCampaignDto joinCampaignDto, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<GetCampaignDto>(_campaignRepository.Join(joinCampaignDto.CampaignUuid, currentUser.Id));
    }

    public async Task<bool> Leave(LeaveCampaignDto joinCampaignDto, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _campaignRepository.Leave(joinCampaignDto.CampaignUuid, currentUser.Id);
    }
}