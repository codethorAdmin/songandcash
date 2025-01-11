using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class ContractRepository(SongAndCashContext context)
    : BaseRepository(context),
        IContractRepository
{
    private readonly SongAndCashContext _context = context;

    public async Task<Contract> CreateContract(Contract contractToCreate)
    {
        _context.Contracts.Add(contractToCreate);
        _ = await _context.SaveChangesAsync();
        return contractToCreate;
    }

    public async Task<Contract> Update(Contract newContract)
    {
        _context.Contracts.Update(newContract);
        _ = await _context.SaveChangesAsync();
        return newContract;
    }
}
