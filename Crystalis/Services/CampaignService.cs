using System.Security.Claims;
using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.Enums;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Crystalis.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public CampaignService(ICampaignRepository campaignRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
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
        var createdCampaign = _campaignService.Add(mappedCampaign);
        
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
        return _mapper.Map<GetCampaignDto>(_campaignService.Join(joinCampaignDto.CampaignUuid, currentUser.Id));
    }
}