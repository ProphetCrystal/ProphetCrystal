using System.ComponentModel.DataAnnotations.Schema;

namespace ProphetCrystal.Models;

public class Organization
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid Uuid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int WorldId { get; set; }
    public int? LocationId { get; set; }
    public required string AuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public World World { get; set; }
    public Location? Location { get; set; }
    public ICollection<Person> People { get; set; }
    public ApplicationUser Author { get; set; }
}