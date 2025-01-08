using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;
using SongAndCash.Repository;

namespace SongAndCash.Service.Business;

public class ContractService(IContractRepository contractRepository) : IContractService
{
    public async Task<Contract> CreateContract(
        ContractDetails contractDetails,
        RecoverableSale recoverableSale
    )
    {
        var contract = new Contract
        {
            RecoverableId = recoverableSale.Id,
            ContractFilePath = Guid.NewGuid().ToString(),
        };

        await contractRepository.CreateContract(contract);

        return contract;
    }
}
