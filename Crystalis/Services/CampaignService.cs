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

    public async Task<List<CampaignDto>> Get(SieveModel sieveModel, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<List<CampaignDto>>(_campaignRepository.Get(sieveModel, user.IsInRole("Admin"), currentUser.Id));
    }

    public CampaignDto Get(int id)
    {
        return _mapper.Map<CampaignDto>(_campaignRepository.Get(id));
    }

    public CampaignDto Get(string id)
    {
        return _mapper.Map<CampaignDto>(_campaignRepository.Get(id));
    }

    public async Task<CampaignDto> Create(CreateCampaignDto campaign, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        var mappedCampaign = _mapper.Map<Campaign>(campaign);
        mappedCampaign.Author = currentUser;
        mappedCampaign.CampaignUsers.Add( new()
        {
            UserId = currentUser.Id,
            Role = CampaignUserRole.GameMaster
        });
        var createdCampaign = _campaignRepository.Create(mappedCampaign);
        
        return _mapper.Map<CampaignDto>(createdCampaign);
    }

    public CampaignDto Update(UpdateCampaignDto campaign)
    {
        var mappedCampaign = _mapper.Map<Campaign>(campaign);
        mappedCampaign.UpdatedAt = DateTime.Now;
        return _mapper.Map<CampaignDto>(_campaignRepository.Update(mappedCampaign));
    }

    public void Delete(int id)
    {
        _campaignRepository.Delete(id);
    }

    public void Delete(string id)
    {
        _campaignRepository.Delete(id);
    }

    public async Task<CampaignDto> Join(GetCampaignDto joinCampaignDto, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<CampaignDto>(_campaignRepository.Join(joinCampaignDto.Uuid, currentUser.Id));
    }

    public async Task<bool> Leave(GetCampaignDto joinCampaignDto, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _campaignRepository.Leave(joinCampaignDto.Uuid, currentUser.Id);
    }
}