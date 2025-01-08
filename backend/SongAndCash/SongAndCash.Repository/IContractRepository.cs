using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public interface IContractRepository
{
    Task<Contract> CreateContract(Contract contractToCreate);
}
