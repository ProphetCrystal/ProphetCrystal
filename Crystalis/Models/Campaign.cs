namespace Crystalis.Models;

public class Campaign
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<CampaignUser> CampaignUsers { get; set; } = new List<CampaignUser>();
    
}