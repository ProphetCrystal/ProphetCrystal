using System.ComponentModel.DataAnnotations.Schema;

namespace Crystalis.Models;

public class Location
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int CampaignID { get; set; }
    
    public Campaign.Campaign Campaign { get; set; }
}