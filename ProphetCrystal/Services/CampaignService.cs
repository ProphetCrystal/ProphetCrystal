using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProphetCrystal.DTO.Campaign;
using ProphetCrystal.Enums;
using ProphetCrystal.Models;
using ProphetCrystal.Repositories.Interfaces;
using ProphetCrystal.Services.Interfaces;
using Sieve.Models;

namespace ProphetCrystal.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public CampaignService(ICampaignRepository campaignRepository, IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _campaignRepository = campaignRepository;
    }

    public async Task<List<CampaignDto>> Get(SieveModel sieveModel, ClaimsPrincipal user)
    {
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<List<CampaignDto>>(_campaignRepository.Get(sieveModel, user.IsInRole("Admin"),
            currentUser.Id));
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
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        Campaign? mappedCampaign = _mapper.Map<Campaign>(campaign);
        mappedCampaign.Author = currentUser;
        mappedCampaign.CampaignUsers.Add(new CampaignUser
        {
            UserId = currentUser.Id,
            Role = CampaignUserRole.GameMaster
        });
        Campaign createdCampaign = _campaignRepository.Create(mappedCampaign);

        return _mapper.Map<CampaignDto>(createdCampaign);
    }

    public CampaignDto Update(UpdateCampaignDto campaign)
    {
        Campaign? mappedCampaign = _mapper.Map<Campaign>(campaign);
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

    public async Task<CampaignDto> Join(GetCampaignDto getCampaignDto, ClaimsPrincipal user)
    {
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<CampaignDto>(_campaignRepository.Join(getCampaignDto.Uuid, currentUser.Id));
    }

    public async Task<bool> Leave(GetCampaignDto getCampaignDto, ClaimsPrincipal user)
    {
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        return _campaignRepository.Leave(getCampaignDto.Uuid, currentUser.Id);
    }
}