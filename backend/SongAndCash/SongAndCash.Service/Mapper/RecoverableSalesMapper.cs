using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;
using SongAndCash.Model.Enum;

namespace SongAndCash.Service.Mapper;

public class RecoverableSalesMapper: IRecoverableSalesMapper
{
    public CreateRecoverableSale MapToCreateRecoverableSale(CreateRecoverableSaleDto createRecoverableSaleDto)
    {
        return new CreateRecoverableSale
        {
            UserId = createRecoverableSaleDto.UserId,
            LastSixMonthsSettlement = createRecoverableSaleDto.LastSixMonthsSettlement,
            EstimatedPaymentToArtist = createRecoverableSaleDto.EstimatedPaymentToArtist,
            EstimatedMonthlyBillingByArtist = createRecoverableSaleDto.EstimatedMonthlyBillingByArtist
        };
    }

    public RecoverableSale FromCreateToRecoverableSale(CreateRecoverableSale createRecoverableSale)
    {
        return new RecoverableSale
        {
            Status = RecoverableSaleStatus.Sent,
            UserId = createRecoverableSale.UserId,
            LastSixMonthsSettlement = createRecoverableSale.LastSixMonthsSettlement,
            EstimatedPaymentToArtist = createRecoverableSale.EstimatedPaymentToArtist,
            EstimatedMonthlyBillingByArtist = createRecoverableSale.EstimatedMonthlyBillingByArtist
        };
    }
}