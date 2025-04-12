using System.Security.Claims;
using AutoMapper;
using Crystalis.DTO.World;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Crystalis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sieve.Models;

namespace Crystalis.Services;

public class WorldService : IWorldService
{
    private readonly IMapper _mapper;
    private readonly INoteRepository _noteRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWorldRepository _worldRepository;

    public WorldService(IWorldRepository worldRepository, IMapper mapper, UserManager<ApplicationUser> userManager,
        INoteRepository noteRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _noteRepository = noteRepository;
        _worldRepository = worldRepository;
    }

    public async Task<List<WorldDto>> Get(SieveModel sieveModel, ClaimsPrincipal user)
    {
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        return _mapper.Map<List<WorldDto>>(_worldRepository.Get(sieveModel, user.IsInRole("Admin"), currentUser.Id));
    }

    public WorldDto Get(int id)
    {
        return _mapper.Map<WorldDto>(_worldRepository.Get(id));
    }

    public WorldDto Get(string id)
    {
        // List<WorldNote> worldNotes = _noteRepository.Get<WorldNote>(id);
        WorldDto worldDto = _mapper.Map<WorldDto>(_worldRepository.Get(id));
        return worldDto;
    }

    public async Task<WorldDto> Create(CreateWorldDto world, ClaimsPrincipal user)
    {
        ApplicationUser? currentUser = await _userManager.GetUserAsync(user);
        World? mappedCampaign = _mapper.Map<World>(world);
        mappedCampaign.Author = currentUser;
        World createdCampaign = _worldRepository.Create(mappedCampaign);

        return _mapper.Map<WorldDto>(createdCampaign);
    }

    public WorldDto Update(UpdateWorldDto world)
    {
        World? mappedCampaign = _mapper.Map<World>(world);
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