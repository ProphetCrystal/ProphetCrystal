using System.Security.Claims;
using Crystalis.DTO.World;
using Sieve.Models;

namespace Crystalis.Services.Interfaces;

public interface IWorldService
{
    public Task<List<WorldDto>> Get(SieveModel sieveModel, ClaimsPrincipal user);
    public WorldDto Get(int id);
    public WorldDto Get(string id);
    public Task<WorldDto> Create(CreateWorldDto world, ClaimsPrincipal user);
    public WorldDto Update(UpdateWorldDto world);
    public void Delete(int id);
    public void Delete(string id);
}