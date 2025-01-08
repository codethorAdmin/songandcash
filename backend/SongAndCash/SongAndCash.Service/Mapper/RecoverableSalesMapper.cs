using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;
using SongAndCash.Model.Enum;

namespace SongAndCash.Service.Mapper;

public class RecoverableSalesMapper : IRecoverableSalesMapper
{
    public CreateRecoverableSale MapToCreateRecoverableSale(
        CreateRecoverableSaleDto createRecoverableSaleDto
    )
    {
        return new CreateRecoverableSale
        {
            UserId = createRecoverableSaleDto.UserId,
            LastSixMonthsSettlement = createRecoverableSaleDto.LastSixMonthsSettlement,
            EstimatedPaymentToArtist = createRecoverableSaleDto.EstimatedPaymentToArtist,
            EstimatedMonthlyBillingByArtist =
                createRecoverableSaleDto.EstimatedMonthlyBillingByArtist,
        };
    }

    public RecoverableSale FromCreateToRecoverableSale(
        CreateRecoverableSale createRecoverableSale,
        int userId
    )
    {
        return new RecoverableSale
        {
            Status = RecoverableSaleStatus.Sent,
            UserId = userId,
            LastSixMonthsSettlement = createRecoverableSale.LastSixMonthsSettlement,
            EstimatedPaymentToArtist = createRecoverableSale.EstimatedPaymentToArtist,
            EstimatedMonthlyBillingByArtist = createRecoverableSale.EstimatedMonthlyBillingByArtist,
        };
    }

    public RecoverableSaleDto MapToRecoverableSaleDto(RecoverableSale recoverableSaleDetails)
    {
        return new RecoverableSaleDto
        {
            Id = recoverableSaleDetails.Id,
            Status = recoverableSaleDetails.Status,
            UserId = recoverableSaleDetails.UserId,
            LastSixMonthsSettlement = recoverableSaleDetails.LastSixMonthsSettlement,
            EstimatedPaymentToArtist = recoverableSaleDetails.EstimatedPaymentToArtist,
            EstimatedMonthlyBillingByArtist =
                recoverableSaleDetails.EstimatedMonthlyBillingByArtist,
            User = recoverableSaleDetails.User,
            EndDate = recoverableSaleDetails.EndDate,
            StartDate = recoverableSaleDetails.StartDate,
            ExtraordinaryEndDate = recoverableSaleDetails.ExtraordinaryEndDate,
            FinalPaymentToArtist = recoverableSaleDetails.FinalPaymentToArtist,
            FinalPaymentToReturn = recoverableSaleDetails.FinalPaymentToReturn,
            RejectionReason = recoverableSaleDetails.RejectionReason,
        };
    }
}
