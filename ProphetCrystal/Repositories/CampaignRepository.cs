using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProphetCrystal.Contexts;
using ProphetCrystal.Models;
using ProphetCrystal.Repositories.Interfaces;
using Sieve.Models;
using Sieve.Services;

namespace ProphetCrystal.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly DataContext _dataContext;
    private readonly ISieveProcessor _sieveProcessor;

    public CampaignRepository(DataContext dataContext, ISieveProcessor sieveProcessor)
    {
        _dataContext = dataContext;
        _sieveProcessor = sieveProcessor;
    }

    public List<Campaign> Get(SieveModel sieveModel, bool isAdmin, string userId)
    {
        IQueryable<Campaign>? result = _dataContext.Campaigns.AsNoTracking();
        if (!isAdmin) result = result.Where(x => x.AuthorId == userId);
        result = _sieveProcessor.Apply(sieveModel, result);
        return result.ToList();
    }

    public Campaign Get(int id)
    {
        return _dataContext.Campaigns.AsNoTracking().First(x => x.Id == id);
    }

    public Campaign Get(string id)
    {
        return _dataContext.Campaigns.AsNoTracking().First(x => x.Uuid == Guid.Parse(id));
    }

    public Campaign Create(Campaign campaign)
    {
        campaign.UpdatedAt = DateTime.UtcNow;
        campaign.Uuid = Guid.NewGuid();
        EntityEntry<Campaign> entry = _dataContext.Add(campaign);
        _dataContext.SaveChanges();
        return entry.Entity;
    }

    public Campaign Update(Campaign campaign)
    {
        Campaign campaignToUpdate = _dataContext.Campaigns.First(x => x.Uuid == campaign.Uuid);
        _dataContext.Campaigns.Entry(campaignToUpdate).CurrentValues.SetValues(campaign);
        _dataContext.SaveChanges();
        return campaignToUpdate;
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

    public bool Leave(string campaignId, string userId)
    {
        Campaign campaign = Get(campaignId);
        CampaignQueue? campaignQueue =
            _dataContext.CampaignQueues.FirstOrDefault(x => x.CampaignId == campaign.Id && x.UserId == userId);
        if (campaignQueue != null)
        {
            _dataContext.CampaignQueues.Remove(campaignQueue);
            _dataContext.SaveChanges();
            return true;
        }

        CampaignUser? campaignUser =
            _dataContext.CampaignUsers.FirstOrDefault(x => x.CampaignId == campaign.Id && x.UserId == userId);
        if (campaignUser != null)
        {
            _dataContext.CampaignUsers.Remove(campaignUser);
            _dataContext.SaveChanges();
            return true;
        }

        return false;
    }

    public Campaign Join(string campaignId, string userId)
    {
        Campaign campaign = Get(campaignId);
        CampaignQueue campaignQueue = new();
        campaignQueue.CampaignId = campaign.Id;
        campaignQueue.UserId = userId;
        _dataContext.Add(campaignQueue);
        _dataContext.SaveChanges();
        return campaignQueue.Campaign;
    }
}