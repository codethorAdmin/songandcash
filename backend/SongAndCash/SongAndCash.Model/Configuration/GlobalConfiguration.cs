namespace SongAndCash.Model.Configuration;

public class GlobalConfiguration
{
    public DatabaseConfiguration Database { get; set; }
    public EmailConfiguration Email { get; set; }
    public DocumentConfiguration Documents { get; set; }
}
