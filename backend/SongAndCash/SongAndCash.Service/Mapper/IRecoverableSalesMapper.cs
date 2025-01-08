using SongAndCash.Model.Dto;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Mapper;

public interface IRecoverableSalesMapper
{
    CreateRecoverableSale MapToCreateRecoverableSale(
        CreateRecoverableSaleDto createRecoverableSaleDto
    );
    RecoverableSale FromCreateToRecoverableSale(
        CreateRecoverableSale createRecoverableSale,
        int userId
    );
    RecoverableSaleDto MapToRecoverableSaleDto(RecoverableSale recoverableSaleDetails);
}
