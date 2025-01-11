namespace SongAndCash.Model.Entity;

public class CreateRecoverableSale
{
    public int UserId { get; set; }

    public List<string> LastSixMonthsSettlement { get; set; } = [];

    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }
}
