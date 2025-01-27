using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public interface IRecoverableSalesService
{
    Task<RecoverableSale> GetRecoverableSale(int userId, int recoverableSaleId);
    Task<RecoverableSale> CreateRecoverableSale(CreateRecoverableSale mapToCreateRecoverableSale);
    Task<List<RecoverableSale>> GetRecoverableSales(int userId);
    Task<bool> MarkUnderStudy(int userId, int recoverableSaleId);
    Task<bool> Reject(int userId, int recoverableSaleId, string? rejectBodyReason);
    Task<bool> PreAcceptByAdmin(int userId, int recoverableSaleId, Proposal proposal);
    Task<bool> AcceptByArtist(int userId, int recoverableSaleId, Proposal proposal);
    Task<Contract> GenerateContract(
        int userId,
        int recoverableSaleId,
        ContractDetails contractDetails
    );

    Task<(byte[] content, string contentType)[]> GetRecoverableSaleLastSixMonthSettlements(
        int userId,
        int recoverableSaleId
    );
}
