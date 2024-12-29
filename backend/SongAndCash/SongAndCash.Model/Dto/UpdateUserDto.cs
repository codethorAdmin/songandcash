namespace SongAndCash.Model.Dto;

public class UpdateUserDto
{
    public required string Username { get; set; }
    public required string SpotifyLink { get; set; }
    
    public bool? IsCompany { get; set; }
    public bool? FiscalIdentificationNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? Iban { get; set; }
}