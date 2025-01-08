using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class RecoverableSalesRepository : IRecoverableSalesRepository
{
    public Task<RecoverableSale> CreateRecoverableSale(RecoverableSale recoverableSaleToCreate)
    {
        throw new NotImplementedException();
    }

    public Task<RecoverableSale> GetRecoverableSale(int recoverableSaleId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RecoverableSale>> GetRecoverableSales(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(RecoverableSale recoverableSale)
    {
        throw new NotImplementedException();
    }
}
