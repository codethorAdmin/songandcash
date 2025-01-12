using Microsoft.AspNetCore.Authentication;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IUserService
{
    Task<User> GetUser(int id);
    Task<User> GetUserByUsername(string username);
    Task<User> CreateUser(CreateUser user);
    Task<User> UpdateUser(int id, UpdateUser user);
    Task<User> LoginOrRegister(AuthenticateResult authenticateResult);
}
