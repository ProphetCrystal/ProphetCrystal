using System.ComponentModel.DataAnnotations.Schema;

namespace ProphetCrystal.Models;

public class Location
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid Uuid { get; set; }
    public  required string Name { get; set; }
    public  required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int WorldId { get; set; }

    public World World { get; set; }
}