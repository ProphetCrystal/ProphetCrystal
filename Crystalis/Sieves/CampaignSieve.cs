using Crystalis.Models;
using Sieve.Services;

namespace Crystalis.Sieves;

public class CampaignSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Campaign>(p => p.Name)
            .CanFilter();
        mapper.Property<Campaign>(p => p.Description)
            .CanFilter();
        mapper.Property<Campaign>(p => p.CreatedAt)
            .CanFilter()
            .CanSort();
        mapper.Property<Campaign>(p => p.UpdatedAt)
            .CanFilter()
            .CanSort();
    }
}