using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Mapper;

public interface IContractMapper
{
    ContractDto MapToContractDto(Contract contract);
}
