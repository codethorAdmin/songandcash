using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IContractService
{
    Task<Contract> CreateContract(RecoverableSale recoverableSale);
}
