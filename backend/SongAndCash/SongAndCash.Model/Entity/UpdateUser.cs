namespace SongAndCash.Model.Entity;

public class UpdateUser
{
    public string? Username { get; set; }
    public string? SpotifyLink { get; set; }

    public bool? IsCompany { get; set; }
    public string? FiscalIdentificationNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? Iban { get; set; }
}
