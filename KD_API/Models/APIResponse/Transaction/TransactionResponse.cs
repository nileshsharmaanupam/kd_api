namespace KD_API.Models.APIResponse.Transaction;

public class TransactionResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int? CattleId { get; set; }
    public string? CattleName { get; set; }
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class TransactionListResponse
{
    public List<TransactionResponse> Transactions { get; set; }
    public int TotalCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageTransactionAmount { get; set; }
    public Dictionary<string, int> StatusCounts { get; set; }
}
