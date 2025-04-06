using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Crystalis.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly DataContext _dataContext;
    public CampaignRepository (DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public List<Campaign> Get()
    {
        throw new NotImplementedException();
    }

    public Campaign Get(int id)
    {
        throw new NotImplementedException();
    }

    public Campaign Get(string id)
    {
        throw new NotImplementedException();
    }

    public Campaign Add(Campaign campaign)
    {
        campaign.UpdatedAt = DateTime.UtcNow;
        campaign.Uuid = Guid.NewGuid();
        EntityEntry<Campaign> entry = _dataContext.Add(campaign);
        _dataContext.SaveChanges();
        return entry.Entity;
    }

    public Campaign Update(Campaign campaign)
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
}