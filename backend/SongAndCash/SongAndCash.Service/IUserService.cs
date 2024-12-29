using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service;

public interface IUserService
{
    Task<User> GetUser(int id);
    Task<User> CreateUser(CreateUser user);
    Task<User> UpdateUser(UpdateUser user);
}