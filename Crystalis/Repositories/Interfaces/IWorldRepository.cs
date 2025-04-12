using Crystalis.Models;
using Sieve.Models;

namespace Crystalis.Repositories.Interfaces;

public interface IWorldRepository
{
    public List<World> Get(SieveModel sieveModel, bool isAdmin, string userId);
    public World Get(int id);
    public World Get(string id);
    public World Create(World world);
    public World Update(World world);
    public void Delete(int id);
    public void Delete(string id);
}