using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IContractService
{
    Task<Contract> CreateContract(ContractDetails contractDetails, RecoverableSale recoverableSale);
}
