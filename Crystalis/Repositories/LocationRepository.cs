using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sieve.Models;
using Sieve.Services;

namespace Crystalis.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DataContext _dataContext;
    private readonly ISieveProcessor _sieveProcessor;

    public LocationRepository(DataContext dataContext, ISieveProcessor sieveProcessor)
    {
        _dataContext = dataContext;
        _sieveProcessor = sieveProcessor;
    }

    public List<Location> Get(SieveModel sieveModel, int locationId)
    {
        IQueryable<Location>? result = _dataContext.Locations.AsNoTracking();
        result = result.Where(x => x.WorldId == locationId);
        result = _sieveProcessor.Apply(sieveModel, result);
        return result.ToList();
    }

    public Location Get(int id)
    {
        return _dataContext.Locations.AsNoTracking().First(x => x.Id == id);
    }

    public Location Get(string id)
    {
        return _dataContext.Locations.AsNoTracking().First(x => x.Uuid == Guid.Parse(id));
    }

    public Location Create(Location location)
    {
        location.UpdatedAt = DateTime.UtcNow;
        location.Uuid = Guid.NewGuid();
        EntityEntry<Location> entry = _dataContext.Add(location);
        _dataContext.SaveChanges();
        return entry.Entity;
    }

    public Location Update(Location location)
    {
        Location worldToUpdate = _dataContext.Locations.First(x => x.Uuid == location.Uuid);
        _dataContext.Locations.Entry(worldToUpdate).CurrentValues.SetValues(location);
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