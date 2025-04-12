namespace Crystalis.Models;

public abstract class Note
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }

    // Navigation property to owner (can be World, Location, or Person)
    public int OwnerId { get; set; }
    public string OwnerType { get; set; } // Discriminator
}

public class WorldNote : Note
{
    public World World { get; set; }
}

public class LocationNote : Note
{
    public Location Location { get; set; }
}