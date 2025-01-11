namespace SongAndCash.Model.Dto;

public class CreateRecoverableSaleDto
{
    public int UserId { get; set; }

    // base64 excel files
    public List<string> LastSixMonthsSettlement { get; set; } = [];

    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }
}
