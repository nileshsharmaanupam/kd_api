using KD_API.Models.Enums;

namespace KD_API.Models.APIResponse.CreditNote;

public class CreditNoteResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreditDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public CreditStatus Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsOverdue { get; set; }
    public int DaysUntilDue { get; set; }
}

public class CreditNoteListResponse
{
    public List<CreditNoteResponse> CreditNotes { get; set; }
    public int TotalCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public Dictionary<string, int> StatusCounts { get; set; }
    public int OverdueCount { get; set; }
}

public class DeleteCreditNoteResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
