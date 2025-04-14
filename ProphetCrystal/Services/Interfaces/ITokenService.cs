using ProphetCrystal.Models;

namespace ProphetCrystal.Services.Interfaces;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user, IList<string> roles);
}