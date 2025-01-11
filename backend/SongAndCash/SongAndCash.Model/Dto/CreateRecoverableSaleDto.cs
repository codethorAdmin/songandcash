namespace SongAndCash.Model.Dto;

public class CreateRecoverableSaleDto
{
    public int UserId { get; set; }

    public List<string> LastSixMonthsSettlement { get; set; } = [];

    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }
}
