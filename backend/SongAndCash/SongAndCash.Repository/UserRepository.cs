using Microsoft.EntityFrameworkCore;
using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class UserRepository(SongAndCashContext dbContext): IUserRepository
{
    public async Task<User?> GetUser(int id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User> CreateUser(User user)
    {
        dbContext.Users.Add(user);
        var result = await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        await dbContext.SaveChangesAsync();
    }
}