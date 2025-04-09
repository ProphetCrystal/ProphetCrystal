using Crystalis.Enums;

namespace Crystalis.Models.Campaign;

public class CampaignUser
{
    public string UserId { get; set; }
    public int CampaignId { get; set; }
    public CampaignUserRole Role { get; set; } // Enum for GM/Player
    
    public ApplicationUser User { get; set; }
    public Models.Campaign.Campaign Campaign { get; set; }
}