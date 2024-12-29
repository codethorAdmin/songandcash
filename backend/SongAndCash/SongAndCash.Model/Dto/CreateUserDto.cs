namespace SongAndCash.Model.Dto;

public class CreateUserDto
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string SpotifyLink { get; set; }
}