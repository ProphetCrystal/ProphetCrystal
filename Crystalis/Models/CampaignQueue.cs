using System.ComponentModel.DataAnnotations.Schema;

namespace Crystalis.Models;

public class CampaignQueue
{
    public string UserId { get; set; }
    public int CampaignId { get; set; }
    
    public ApplicationUser User { get; set; }
    public Campaign Campaign { get; set; }
}