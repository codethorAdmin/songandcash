using Microsoft.EntityFrameworkCore;
using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class RecoverableSalesRepository(SongAndCashContext dbContext) : IRecoverableSalesRepository
{
    public async Task<RecoverableSale> CreateRecoverableSale(
        RecoverableSale recoverableSaleToCreate
    )
    {
        dbContext.RecoverableSales.Add(recoverableSaleToCreate);
        var result = await dbContext.SaveChangesAsync();
        return recoverableSaleToCreate;
    }

    public async Task<RecoverableSale> GetRecoverableSale(int recoverableSaleId)
    {
        return await dbContext.RecoverableSales.FindAsync(recoverableSaleId);
    }

    public async Task<List<RecoverableSale>> GetRecoverableSales(int userId)
    {
        return await dbContext.RecoverableSales.ToListAsync();
    }

    public Task<bool> Update(RecoverableSale recoverableSale)
    {
        throw new NotImplementedException();
    }
}
