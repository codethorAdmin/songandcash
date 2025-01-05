using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IRecoverableSalesService
{
    Task<RecoverableSale> GetRecoverableSale(int id);
    Task<RecoverableSale> CreateRecoverableSale(CreateRecoverableSale mapToCreateRecoverableSale);
}