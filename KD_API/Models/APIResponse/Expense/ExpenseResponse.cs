using KD_API.Models;

namespace KD_API.Models.APIResponse.Expense;

public class ExpenseResponse
{
    public int Id { get; set; }
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTag? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ExpenseListResponse
{
    public List<ExpenseResponse> Expenses { get; set; }
    public int TotalCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageExpenseAmount { get; set; }
    public Dictionary<string, decimal> ExpensesByTag { get; set; }
    public Dictionary<string, int> ExpenseCountByTag { get; set; }
}
