using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IContractRepository : IBaseRepository
{
    Task<Contract> CreateContract(Contract contractToCreate);
    Task<Contract> Update(Contract newContract);
}
