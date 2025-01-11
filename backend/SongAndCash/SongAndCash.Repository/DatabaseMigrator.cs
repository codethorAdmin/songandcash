using Microsoft.EntityFrameworkCore;

namespace SongAndCash.Repository;

public static class DatabaseMigrator
{
    public static async Task MigrateDatabase(SongAndCashContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
    }
}
