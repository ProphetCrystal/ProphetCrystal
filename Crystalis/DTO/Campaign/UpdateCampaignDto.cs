namespace Crystalis.DTO.Campaign;

public class UpdateCampaignDto
{
    public required string Uuid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}