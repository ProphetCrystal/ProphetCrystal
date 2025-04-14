using System.ComponentModel.DataAnnotations.Schema;

namespace ProphetCrystal.Models;

public class Character
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Bio { get; set; }
    public required string CharacterSheetJson { get; set; }
    public bool IsActive { get; set; } = true;
    public string UserId { get; set; }

    public int CampaignId { get; set; }

    public ApplicationUser User { get; set; }
    public Campaign Campaign { get; set; }
}