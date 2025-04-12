using Crystalis.Models;
using Sieve.Models;

namespace Crystalis.Repositories.Interfaces;

public interface ILocationRepository
{
    public List<Location> Get(SieveModel sieveModel, int worldId);
    public Location Get(int id);
    public Location Get(string id);
    public Location Create(Location campaign);
    public Location Update(Location campaign);
    public void Delete(int id);
    public void Delete(string id);
}