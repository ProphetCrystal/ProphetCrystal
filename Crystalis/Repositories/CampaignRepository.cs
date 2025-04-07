using Crystalis.Contexts;
using Crystalis.Models;
using Crystalis.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sieve.Models;
using Sieve.Services;

namespace Crystalis.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly DataContext _dataContext;
    private readonly ISieveProcessor _sieveProcessor;
    public CampaignRepository (DataContext dataContext, ISieveProcessor sieveProcessor)
    {
        _dataContext = dataContext;
        _sieveProcessor = sieveProcessor;
    }
    public List<Campaign> Get(SieveModel sieveModel, bool isAdmin, string userId)
    {
        var result = _dataContext.Campaigns.AsNoTracking();
        if (!isAdmin)
        {
            result = result.Where(x => x.AuthorId == userId);
        }
        result = _sieveProcessor.Apply(sieveModel, result);
        return result.ToList();
    }

    public Campaign Get(int id)
    {
        throw new NotImplementedException();
    }

    public Campaign Get(string id)
    {
        return _dataContext.Campaigns.First(x => x.Uuid == Guid.Parse(id));
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
    public bool Leave(string campaignId, string userId)
    {
        var campaign = this.Get(campaignId);
        CampaignQueue? campaignQueue = _dataContext.CampaignQueues.FirstOrDefault((x) => x.CampaignId == campaign.Id && x.UserId == userId);
        if (campaignQueue != null)
        {
            _dataContext.CampaignQueues.Remove(campaignQueue);
            _dataContext.SaveChanges();
            return true;
        }

        CampaignUser? campaignUser = _dataContext.CampaignUsers.FirstOrDefault((x) => x.CampaignId == campaign.Id && x.UserId == userId);
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
        var campaign = this.Get(campaignId);
        CampaignQueue campaignQueue = new CampaignQueue();
        campaignQueue.CampaignId = campaign.Id;
        campaignQueue.UserId = userId;
        _dataContext.Add(campaignQueue);
        _dataContext.SaveChanges();
        return campaignQueue.Campaign;
    }
}