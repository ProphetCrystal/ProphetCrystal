using Microsoft.AspNetCore.Identity;

namespace Crystalis.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }


    public ICollection<CampaignUser> CampaignUsers { get; set; }
    public ICollection<CampaignQueue> CampaignQueues { get; set; }
    public ICollection<Character> Characters { get; set; }
    public ICollection<Campaign> Campaigns { get; set; }
}