using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IUserRepository
{
    Task<User> GetUser(int id);
    Task<User> CreateUser(CreateUser createUser);
    Task UpdateUser(User user);
}