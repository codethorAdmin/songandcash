using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IRecoverableSalesRepository
{
    Task<RecoverableSale> CreateRecoverableSale(RecoverableSale recoverableSaleToCreate);
}