using ProphetCrystal.Enums;

namespace ProphetCrystal.Models;

public class CampaignUser
{
    public string UserId { get; set; }
    public int CampaignId { get; set; }
    public CampaignUserRole Role { get; set; } // Enum for GM/Player

    public ApplicationUser User { get; set; }
    public Campaign Campaign { get; set; }
}