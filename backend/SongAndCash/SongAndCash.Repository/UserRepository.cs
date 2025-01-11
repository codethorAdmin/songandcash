using Microsoft.EntityFrameworkCore;
using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class UserRepository(SongAndCashContext dbContext)
    : BaseRepository(dbContext),
        IUserRepository
{
    private readonly SongAndCashContext _dbContext = dbContext;

    public async Task<User?> GetUser(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User> CreateUser(User user)
    {
        _dbContext.Users.Add(user);
        var result = await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        await _dbContext.SaveChangesAsync();
    }
}
