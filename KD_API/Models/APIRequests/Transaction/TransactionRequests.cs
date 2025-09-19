namespace KD_API.Models.APIRequests.Transaction;

public class CreateTransaction
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

public class UpdateTransaction
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
