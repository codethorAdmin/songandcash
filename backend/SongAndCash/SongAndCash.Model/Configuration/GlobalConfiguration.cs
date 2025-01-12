namespace SongAndCash.Model.Configuration;

public class GlobalConfiguration
{
    public string UrlApp { get; set; }
    public DatabaseConfiguration Database { get; set; }
    public EmailConfiguration Email { get; set; }
    public DocumentConfiguration Documents { get; set; }
    public JwtConfiguration Jwt { get; set; }
    public AuthenticationConfiguration Authentication { get; set; }
}
