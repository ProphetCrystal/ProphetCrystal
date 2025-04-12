using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sieve.Models;
using Sieve.Services;

namespace Crystalis.Repositories;

public class WorldRepository : IWorldRepository
{
    private readonly DataContext _dataContext;
    private readonly ISieveProcessor _sieveProcessor;

    public WorldRepository(DataContext dataContext, ISieveProcessor sieveProcessor)
    {
        _dataContext = dataContext;
        _sieveProcessor = sieveProcessor;
    }

    public List<World> Get(SieveModel sieveModel, bool isAdmin, string userId)
    {
        IQueryable<World>? result = _dataContext.Worlds.AsNoTracking();
        if (!isAdmin) result = result.Where(x => x.AuthorId == userId);
        result = _sieveProcessor.Apply(sieveModel, result);
        return result.ToList();
    }

    public World Get(int id)
    {
        return _dataContext.Worlds.AsNoTracking().First(x => x.Id == id);
    }

    public World Get(string id)
    {
        return _dataContext.Worlds.Include(x => x.Notes).AsNoTracking().First(x => x.Uuid == Guid.Parse(id));
    }

    public World Create(World world)
    {
        world.UpdatedAt = DateTime.UtcNow;
        world.Uuid = Guid.NewGuid();
        EntityEntry<World> entry = _dataContext.Add(world);
        _dataContext.SaveChanges();
        return entry.Entity;
    }

    public World Update(World world)
    {
        World worldToUpdate = _dataContext.Worlds.First(x => x.Uuid == world.Uuid);
        _dataContext.Worlds.Entry(worldToUpdate).CurrentValues.SetValues(world);
        _dataContext.SaveChanges();
        return worldToUpdate;
    }

    public void Delete(int id)
    {
        _dataContext.Remove(_dataContext.Campaigns.First(x => x.Id == id));
        _dataContext.SaveChanges();
    }

    public void Delete(string id)
    {
        _dataContext.Remove(_dataContext.Campaigns.First(x => x.Uuid == Guid.Parse(id)));
        _dataContext.SaveChanges();
    }
}