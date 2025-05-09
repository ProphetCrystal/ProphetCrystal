using System.ComponentModel.DataAnnotations.Schema;

namespace ProphetCrystal.Models;

public class World
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid Uuid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public required string AuthorId { get; set; }

    public ICollection<Campaign> Campaigns { get; set; }
    public ICollection<Location> Locations { get; set; }
    public ICollection<Person> People { get; set; }
    public ICollection<Organization> Organizations { get; set; }
    public ICollection<Note> Notes { get; set; }
    public ApplicationUser Author { get; set; }
}