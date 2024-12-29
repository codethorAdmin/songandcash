using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Mapper;

public interface IUserMapper
{
    UserDto MapToUserDto(User user);

    User MapToUser(UserDto userDto);

    CreateUserDto MapToCreateUserDto(CreateUser user);

    CreateUser MapToCreateUser(CreateUserDto userDto);
    
    UpdateUserDto MapToUpdateUserDto(UpdateUser user);

    UpdateUser MapToUpdateUser(UpdateUserDto userDto);
}