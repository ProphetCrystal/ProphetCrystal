using System.ComponentModel.DataAnnotations.Schema;

namespace Crystalis.Models;

public class Campaign
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string AuthorId { get; set; }
    
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<CampaignUser> CampaignUsers { get; set; } = new List<CampaignUser>();
    public ICollection<CampaignQueue> CampaignQueues { get; set; } = new List<CampaignQueue>();
    public ApplicationUser Author { get; set; }
    
}