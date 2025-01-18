using System.Text.Json.Serialization;

namespace SongAndCash.Model.Dto;

public class UpdateUserDto
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("spotifyLink")]
    public string? SpotifyLink { get; set; }

    [JsonPropertyName("isCompany")]
    public bool? IsCompany { get; set; }

    [JsonPropertyName("fiscalIdentificationNumber")]
    public string? FiscalIdentificationNumber { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; }

    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }

    [JsonPropertyName("iban")]
    public string? Iban { get; set; }
}
