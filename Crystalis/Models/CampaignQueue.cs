namespace Crystalis.Models.Campaign;

public class CampaignQueue
{
    public string UserId { get; set; }
    public int CampaignId { get; set; }
    
    public ApplicationUser User { get; set; }
    public Models.Campaign.Campaign Campaign { get; set; }
}