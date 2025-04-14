namespace ProphetCrystal.Configuration;

public class JwtSection
{
    public required string SecretKey { get; set; }
    public required string ValidIssuer { get; set; }
    public required string ValidAudience { get; set; }
    public int ExpiryInMinutes { get; set; }
}