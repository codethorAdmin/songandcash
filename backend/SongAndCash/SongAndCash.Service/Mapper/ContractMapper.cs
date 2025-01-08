using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Mapper;

public class ContractMapper(IRecoverableSalesMapper recoverableSalesMapper) : IContractMapper
{
    public ContractDto MapToContractDto(Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            RecoverableSale = recoverableSalesMapper.MapToRecoverableSaleDto(
                contract.RecoverableSale
            ),
            RecoverableId = contract.RecoverableId,
        };
    }
}
