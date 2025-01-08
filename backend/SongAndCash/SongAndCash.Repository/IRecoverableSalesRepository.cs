using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IRecoverableSalesRepository
{
    Task<RecoverableSale> CreateRecoverableSale(RecoverableSale recoverableSaleToCreate);
    Task<RecoverableSale> GetRecoverableSale(int recoverableSaleId);
    Task<List<RecoverableSale>> GetRecoverableSales(int userId);
    Task<bool> Update(RecoverableSale recoverableSale);
}
