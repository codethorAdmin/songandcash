namespace SongAndCash.Model.Entity;

public class Contract
{
    public int Id { get; set; }
    public int RecoverableId { get; set; }
    public RecoverableSale RecoverableSale { get; set; }
    public string ContractFilePath { get; set; }
}
