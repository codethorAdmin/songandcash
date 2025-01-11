namespace SongAndCash.Model.Entity;

public class Contract
{
    public int Id { get; set; }
    public int? RecoverableSaleId { get; set; }
    public RecoverableSale? RecoverableSale { get; set; }
    public string DocumentId { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? FiscalNumber { get; set; }
    public string? IBAN { get; set; }
    public string? Swift { get; set; }
    public string? CountryOfResidence { get; set; }
    public string? CompleteAddress { get; set; }
}
