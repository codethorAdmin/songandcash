using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class RecoverableSalesRepository: IRecoverableSalesRepository
{
    public Task<RecoverableSale> CreateRecoverableSale(RecoverableSale recoverableSaleToCreate)
    {
        throw new NotImplementedException();
    }
}