namespace SongAndCash.Model.Dto;

public class ContractDto
{
    public int Id { get; set; }
    public int? RecoverableSaleId { get; set; }
    public RecoverableSaleDto? RecoverableSale { get; set; }
    public string ContractFilePath { get; set; }
}
