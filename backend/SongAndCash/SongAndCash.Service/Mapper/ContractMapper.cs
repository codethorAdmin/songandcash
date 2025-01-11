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
            RecoverableSaleId = contract.RecoverableSaleId,
        };
    }

    public ContractDetails FromContractDetailsDtoToContractDetails(
        ContractDetailsDto contractDetailsDto
    )
    {
        return new ContractDetails
        {
            Name = contractDetailsDto.Name,
            Swift = contractDetailsDto.Swift,
            CompleteAddress = contractDetailsDto.CompleteAddress,
            FiscalNumber = contractDetailsDto.FiscalNumber,
            LastName = contractDetailsDto.LastName,
            CountryOfResidence = contractDetailsDto.CountryOfResidence,
            DateOfBirth = contractDetailsDto.DateOfBirth,
            IBAN = contractDetailsDto.IBAN,
        };
    }
}
