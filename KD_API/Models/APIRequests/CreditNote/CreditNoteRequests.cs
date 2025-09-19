using KD_API.Models.Enums;

namespace KD_API.Models.APIRequests.CreditNote;

public class GetCreditNoteByIdRequest
{
    public int CreditNoteId { get; set; }
}

public class GetAllCreditNotesRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateCreditNoteRequest
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

public class UpdateCreditNoteRequest
{
    public int CreditNoteId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreditDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public CreditStatus Status { get; set; }
    public string Notes { get; set; }
}

public class DeleteCreditNoteRequest
{
    public int CreditNoteId { get; set; }
}

// Legacy classes for backward compatibility - can be removed later
public class CreateCreditNote : CreateCreditNoteRequest { }
public class UpdateCreditNote : UpdateCreditNoteRequest { }
