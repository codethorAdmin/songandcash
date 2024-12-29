using SongAndCash.Model.Entity;

namespace SongAndCash.Service;

public class UserService: IUserService
{
    public async Task<User> GetUser(int id)
    {
        return new User
        {
            Email = null,
            Username = null,
            SpotifyLink = null
        };
    }

    public Task<User> CreateUser(CreateUser user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(UpdateUser user)
    {
        throw new NotImplementedException();
    }
}