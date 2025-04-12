using Crystalis.Models;
using Sieve.Services;

namespace Crystalis.Sieves;

public class WorldSieve : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<World>(p => p.Name)
            .CanFilter();
        mapper.Property<World>(p => p.Description)
            .CanFilter();
        mapper.Property<World>(p => p.CreatedAt)
            .CanFilter()
            .CanSort();
        mapper.Property<World>(p => p.UpdatedAt)
            .CanFilter()
            .CanSort();
    }
}