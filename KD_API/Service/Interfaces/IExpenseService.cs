using KD_API.Models.APIRequests.Expense;
using KD_API.Models.APIResponse.Expense;

namespace KD_API.Service.Interfaces;

public interface IExpenseService
{
    public Task<ExpenseResponse> GetExpenseById(GetExpenseByIdRequest request);
    public Task<ExpenseListResponse> GetAllExpenses(GetAllExpensesRequest request);
    public Task<ExpenseResponse> CreateExpense(CreateExpenseRequest request);
    public Task<ExpenseResponse> UpdateExpense(UpdateExpenseRequest request);
    public Task<DeleteExpenseResponse> DeleteExpense(DeleteExpenseRequest request);
}
