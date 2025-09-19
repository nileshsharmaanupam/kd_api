using KD_API.Models;

namespace KD_API.Models.APIRequests.Expense;

public class CreateExpense
{
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTagDTO? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class UpdateExpense
{
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTagDTO? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
}
