using Crystalis.DTO.Note;

namespace Crystalis.DTO.World;

public class WorldDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid Uuid { get; set; }
    public required ICollection<NoteDto> Notes { get; set; }
}