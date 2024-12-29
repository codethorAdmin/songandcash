using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Mapper;

public class UserMapper: IUserMapper
{
    public UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth,
            FiscalIdentificationNumber = user.FiscalIdentificationNumber,
            IsCompany = user.IsCompany,
            Nationality = user.Nationality,
            SpotifyLink = user.SpotifyLink,
            Iban = user.Iban,
        };
    }

    public User MapToUser(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Username = userDto.Username,
            DateOfBirth = userDto.DateOfBirth,
            FiscalIdentificationNumber = userDto.FiscalIdentificationNumber,
            IsCompany = userDto.IsCompany,
            Nationality = userDto.Nationality,
            SpotifyLink = userDto.SpotifyLink,
            Iban = userDto.Iban,
        };
    }

    public CreateUserDto MapToCreateUserDto(CreateUser user)
    {
        return new CreateUserDto
        {
            Email = user.Email,
            Username = user.Username,
            SpotifyLink = user.SpotifyLink
        };
    }

    public CreateUser MapToCreateUser(CreateUserDto userDto)
    {
        return new CreateUser
        {
            Email = userDto.Email,
            Username = userDto.Username,
            SpotifyLink = userDto.SpotifyLink
        };    
    }

    public UpdateUserDto MapToUpdateUserDto(UpdateUser user)
    {
        return new UpdateUserDto
        {
            DateOfBirth = user.DateOfBirth,
            FiscalIdentificationNumber = user.FiscalIdentificationNumber,
            IsCompany = user.IsCompany,
            Nationality = user.Nationality,
            SpotifyLink = user.SpotifyLink,
            Username = user.Username
        };
    }

    public UpdateUser MapToUpdateUser(UpdateUserDto userDto)
    {
        return new UpdateUser
        {
            DateOfBirth = userDto.DateOfBirth,
            FiscalIdentificationNumber = userDto.FiscalIdentificationNumber,
            IsCompany = userDto.IsCompany,
            Nationality = userDto.Nationality,
            SpotifyLink = userDto.SpotifyLink,
            Username = userDto.Username
        };    
    }
}