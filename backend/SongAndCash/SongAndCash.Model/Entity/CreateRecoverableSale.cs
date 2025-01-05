namespace SongAndCash.Model.Entity;

public class CreateRecoverableSale
{
    public int UserId { get; set; }

    public IList<object> LastSixMonthsSettlement { get; set; } = [];
    
    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }
}