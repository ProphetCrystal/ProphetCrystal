namespace Crystalis.Models;

public class Location
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public Campaign Campaign { get; set; }
    public int CampaignID { get; set; }
}