using KD_API.Models;

namespace KD_API.Models.APIRequests.Expense;

public class GetExpenseByIdRequest
{
    public int ExpenseId { get; set; }
}

public class GetAllExpensesRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateExpenseRequest
{
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTagDTO? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class UpdateExpenseRequest
{
    public int ExpenseId { get; set; }
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTagDTO? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class DeleteExpenseRequest
{
    public int ExpenseId { get; set; }
}

// Legacy classes for backward compatibility
public class CreateExpense : CreateExpenseRequest { }
public class UpdateExpense : UpdateExpenseRequest { }
