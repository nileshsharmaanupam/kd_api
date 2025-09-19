using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface IExpenseService
{
    public Task<ExpenseDTO> GetExpenseById(int expenseId);
    public Task<IEnumerable<ExpenseDTO>> GetAllExpenses();
    public Task<bool> CreateExpense(ExpenseDTO expenseDto);
    public Task<ExpenseDTO> UpdateExpense(int expenseId, ExpenseDTO expenseDto);
    public Task<bool> DeleteExpense(int expenseId);
}
