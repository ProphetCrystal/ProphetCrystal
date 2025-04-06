namespace Crystalis.Configuration;

public class JwtSection
{
    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public int ExpiryInMinutes { get; set; }
}