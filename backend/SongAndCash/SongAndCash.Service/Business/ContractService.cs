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
            RecoverableSaleId = recoverableSale.Id,
            DocumentId = Guid.NewGuid().ToString(),
        };

        await contractRepository.CreateContract(contract);

        return contract;
    }

    public async Task Update(Contract newContract)
    {
        await contractRepository.Update(newContract);
    }
}
