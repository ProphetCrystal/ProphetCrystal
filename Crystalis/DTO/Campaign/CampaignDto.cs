using Crystalis.Models;

namespace Crystalis.DTO.Campaign;

public class CampaignDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Uuid { get; set; }
    public ICollection<WorldNote> Notes { get; set; }
}