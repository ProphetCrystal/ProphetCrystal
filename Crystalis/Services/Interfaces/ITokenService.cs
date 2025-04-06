using Crystalis.Models;

namespace Crystalis.Services.Interfaces;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user, IList<string> roles);
}