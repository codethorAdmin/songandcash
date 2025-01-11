using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IUserRepository : IBaseRepository
{
    Task<User?> GetUser(int id);
    Task<User?> GetUserByUsername(string username);
    Task<User> CreateUser(User createUser);
    Task UpdateUser(User user);
}
