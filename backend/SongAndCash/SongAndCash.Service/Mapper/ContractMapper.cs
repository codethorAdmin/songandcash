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
            DocumentId = contract.DocumentId,
            Name = contract.Name,
            LastName = contract.LastName,
            DateOfBirth = contract.DateOfBirth,
            FiscalNumber = contract.FiscalNumber,
            IBAN = contract.IBAN,
            Swift = contract.Swift,
            CountryOfResidence = contract.CountryOfResidence,
            CompleteAddress = contract.CompleteAddress,
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
