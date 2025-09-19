namespace KD_API.Models.APIRequests.Transaction;

public class GetTransactionByIdRequest
{
    public int TransactionId { get; set; }
}

public class GetAllTransactionsRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateTransactionRequest
{
    public int CustomerId { get; set; }
    public int? CattleId { get; set; }
    public int? ProductId { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Status { get; set; }
}

public class UpdateTransactionRequest
{
    public int TransactionId { get; set; }
    public int CustomerId { get; set; }
    public int? CattleId { get; set; }
    public int? ProductId { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Status { get; set; }
}

public class DeleteTransactionRequest
{
    public int TransactionId { get; set; }
}

// Legacy classes for backward compatibility
public class CreateTransaction : CreateTransactionRequest { }
public class UpdateTransaction : UpdateTransactionRequest { }
