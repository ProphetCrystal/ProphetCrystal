using Crystalis.DTO.Note;

namespace Crystalis.DTO.World;

public class WorldDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Uuid { get; set; }
    public ICollection<NoteDto> Notes { get; set; }
}