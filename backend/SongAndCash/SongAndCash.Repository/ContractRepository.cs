using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class ContractRepository : IContractRepository
{
    public Task<Contract> CreateContract(Contract contractToCreate)
    {
        throw new NotImplementedException();
    }
}
