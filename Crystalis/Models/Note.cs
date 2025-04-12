using Crystalis.Enums;

namespace Crystalis.Models;

public abstract class Note
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public NoteType OwnerType { get; set; }
}

public class WorldNote : Note
{
    public World World { get; set; }
}

public class LocationNote : Note
{
    public Location Location { get; set; }
}