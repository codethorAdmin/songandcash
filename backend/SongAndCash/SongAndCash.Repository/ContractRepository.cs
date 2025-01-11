using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class ContractRepository(SongAndCashContext context) : IContractRepository
{
    public async Task<Contract> CreateContract(Contract contractToCreate)
    {
        context.Contracts.Add(contractToCreate);
        _ = await context.SaveChangesAsync();
        return contractToCreate;
    }
}
