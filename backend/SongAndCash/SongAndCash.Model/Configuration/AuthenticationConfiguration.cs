namespace SongAndCash.Model.Configuration;

public class AuthenticationConfiguration
{
    public GoogleAuthenticationConfiguration Google { get; set; }
}

public class GoogleAuthenticationConfiguration
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
