using System.Security.Claims;
using AutoMapper;
using Crystalis.DTO.Campaign;
using Crystalis.DTO.World;
using Crystalis.Enums;
using Crystalis.Models;
using Crystalis.Models.Campaign;
using Crystalis.Models.World;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sieve.Models;

namespace Crystalis.Services;

public class WorldService : IWorldService
{
    private readonly IWorldRepository _worldRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public WorldService(IWorldRepository worldRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _worldRepository = worldRepository;
    }

    public async Task<List<WorldDto>> Get(SieveModel sieveModel, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<List<WorldDto>>(_worldRepository.Get(sieveModel, user.IsInRole("Admin"), currentUser.Id));
    }

    public WorldDto Get(int id)
    {
        return _mapper.Map<WorldDto>(_worldRepository.Get(id));
    }

    public WorldDto Get(string id)
    {
        return _mapper.Map<WorldDto>(_worldRepository.Get(id));
    }

    public async Task<WorldDto> Create(CreateWorldDto world, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        var mappedCampaign = _mapper.Map<World>(world);
        mappedCampaign.Author = currentUser;
        var createdCampaign = _worldRepository.Create(mappedCampaign);
        
        return _mapper.Map<WorldDto>(createdCampaign);
    }

    public WorldDto Update(UpdateWorldDto world)
    {
        var mappedCampaign = _mapper.Map<World>(world);
        mappedCampaign.UpdatedAt = DateTime.Now;
        return _mapper.Map<WorldDto>(_worldRepository.Update(mappedCampaign));
    }

    public void Delete(int id)
    {
        _worldRepository.Delete(id);
    }

    public void Delete(string id)
    {
        _worldRepository.Delete(id);
    }
}