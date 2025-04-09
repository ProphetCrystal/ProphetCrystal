using System.ComponentModel.DataAnnotations.Schema;
namespace Crystalis.Models.World;

public class World
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string AuthorId { get; set; }
    
    public ICollection<Campaign.Campaign> Campaigns { get; set; }
    public ApplicationUser Author { get; set; }
    
}