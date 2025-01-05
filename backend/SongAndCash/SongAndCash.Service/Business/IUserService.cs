using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IUserService
{
    Task<User> GetUser(int id);
    Task<User> CreateUser(CreateUser user);
    Task<User> UpdateUser(int id, UpdateUser user);
}