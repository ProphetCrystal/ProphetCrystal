using Crystalis.Enums;
using Crystalis.Models;

namespace Crystalis.Services.Interfaces;

public interface ICampaignService
{
    public List<Campaign> Get();
    public Campaign Get(int id);
    public Campaign Get(string id);
    public Campaign Add(Campaign campaign);
    public Campaign Update(Campaign campaign);
    public void Delete(int id);
    public void Delete(string id);
}