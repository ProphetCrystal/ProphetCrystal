using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Crystalis.Sieves;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(
        IOptions<SieveOptions> options) 
        : base(options)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        return mapper
            .ApplyConfiguration<CampaignSieve>();       
    }
}