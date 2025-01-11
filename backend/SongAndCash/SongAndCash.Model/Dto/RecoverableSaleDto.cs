using SongAndCash.Model.Entity;
using SongAndCash.Model.Enum;

namespace SongAndCash.Model.Dto;

public class RecoverableSaleDto
{
    public int Id { get; set; }
    public RecoverableSaleStatus Status { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    // base64 excel files
    public List<string> LastSixMonthsSettlement { get; set; } = [];

    public double EstimatedMonthlyBillingByArtist { get; set; }
    public double EstimatedPaymentToArtist { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ExtraordinaryEndDate { get; set; }

    public double FinalPaymentToArtist { get; set; }
    public double FinalPaymentToReturn { get; set; }

    public string? RejectionReason { get; set; }
}
