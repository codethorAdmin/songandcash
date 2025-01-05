namespace SongAndCash.Model.Dto;

public class CreateRecoverableSaleDto
{
    public int UserId { get; set; }
    
    public IList<object> LastSixMonthsSettlement { get; set; } = [];
    
    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }
}