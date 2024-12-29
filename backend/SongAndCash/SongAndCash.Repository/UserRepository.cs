using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class UserRepository: IUserRepository
{
    public Task<User> GetUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateUser(CreateUser createUser)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}