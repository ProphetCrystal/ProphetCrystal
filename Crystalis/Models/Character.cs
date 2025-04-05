namespace Crystalis.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string CharacterSheetJson { get; set; } // Flexible storage
    
    // Relationships
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    
    public bool IsActive { get; set; } = true;
}