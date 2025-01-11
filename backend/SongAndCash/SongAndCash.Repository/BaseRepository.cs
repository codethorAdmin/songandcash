namespace SongAndCash.Repository;

public class BaseRepository(SongAndCashContext context)
{
    public Task RunTransaction()
    {
        return context.Database.BeginTransactionAsync();
    }
}
