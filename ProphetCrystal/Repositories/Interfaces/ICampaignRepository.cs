using ProphetCrystal.Models;
using Sieve.Models;

namespace ProphetCrystal.Repositories.Interfaces;

public interface ICampaignRepository
{
    public List<Campaign> Get(SieveModel sieveModel, bool isAdmin, string userId);
    public Campaign Get(int id);
    public Campaign Get(string id);
    public Campaign Create(Campaign campaign);
    public Campaign Update(Campaign campaign);
    public void Delete(int id);
    public void Delete(string id);
    public Campaign Join(string campaignId, string userId);
    public bool Leave(string campaignId, string userId);
}