using Microsoft.EntityFrameworkCore;
using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class RecoverableSalesRepository(SongAndCashContext dbContext)
    : BaseRepository(dbContext),
        IRecoverableSalesRepository
{
    private readonly SongAndCashContext _dbContext = dbContext;

    public async Task<RecoverableSale> CreateRecoverableSale(
        RecoverableSale recoverableSaleToCreate
    )
    {
        _dbContext.RecoverableSales.Add(recoverableSaleToCreate);
        var result = await _dbContext.SaveChangesAsync();
        return recoverableSaleToCreate;
    }

    public async Task<RecoverableSale> GetRecoverableSale(int recoverableSaleId)
    {
        return await _dbContext.RecoverableSales.FindAsync(recoverableSaleId);
    }

    public async Task<List<RecoverableSale>> GetRecoverableSales(int userId)
    {
        return await _dbContext.RecoverableSales.ToListAsync();
    }

    public async Task<bool> Update(RecoverableSale recoverableSale)
    {
        var result = await _dbContext.SaveChangesAsync();
        return result >= 1;
    }
}
