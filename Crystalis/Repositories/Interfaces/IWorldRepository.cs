using Crystalis.Enums;
using Crystalis.Models;
using Crystalis.Models.Campaign;
using Crystalis.Models.World;
using Sieve.Models;

namespace Crystalis.Repositories.Interfaces;

public interface IWorldRepository
{
    public List<World> Get(SieveModel sieveModel, bool isAdmin, string userId);
    public World Get(int id);
    public World Get(string id);
    public World Create(World campaign);
    public World Update(World campaign);
    public void Delete(int id);
    public void Delete(string id);
}