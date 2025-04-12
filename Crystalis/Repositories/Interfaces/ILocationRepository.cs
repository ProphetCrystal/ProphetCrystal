using Crystalis.Models;
using Sieve.Models;

namespace Crystalis.Repositories.Interfaces;

public interface ILocationRepository
{
    public List<Location> Get(SieveModel sieveModel, int locationId);
    public Location Get(int id);
    public Location Get(string id);
    public Location Create(Location location);
    public Location Update(Location location);
    public void Delete(int id);
    public void Delete(string id);
}