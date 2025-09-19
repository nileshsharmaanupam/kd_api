using KD_API.Models.Enums;

namespace KD_API.Models.APIRequests.CreditNote;

public class CreateCreditNote
{
    public int CustomerId { get; set; }
    public DateTime CreditDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public CreditStatus Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class UpdateCreditNote
{
    public int CustomerId { get; set; }
    public DateTime CreditDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public CreditStatus Status { get; set; }
    public string Notes { get; set; }
}
