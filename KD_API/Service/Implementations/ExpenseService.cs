using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Expense;
using KD_API.Models.APIResponse.Expense;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class ExpenseService : IExpenseService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExpenseResponse> GetExpenseById(GetExpenseByIdRequest request)
    {
        var expense = await _context.Expenses.FindAsync(request.ExpenseId);
        if (expense == null)
        {
            throw new ArgumentException($"Expense with ID {request.ExpenseId} not found.");
        }
        
        var response = _mapper.Map<ExpenseResponse>(expense);
        return response;
    }

    public async Task<ExpenseListResponse> GetAllExpenses(GetAllExpensesRequest request)
    {
        var expenses = await _context.Expenses.ToListAsync();
        return _mapper.Map<ExpenseListResponse>(expenses);
    }

    public async Task<ExpenseResponse> CreateExpense(CreateExpenseRequest request)
    {
        var expense = _mapper.Map<ExpenseDTO>(request);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<ExpenseResponse>(expense);
        return response;
    }

    public async Task<ExpenseResponse> UpdateExpense(UpdateExpenseRequest request)
    {
        var existingExpense = await _context.Expenses.FindAsync(request.ExpenseId);
        if (existingExpense == null)
        {
            throw new ArgumentException($"Expense with ID {request.ExpenseId} not found.");
        }
        
        _mapper.Map(request, existingExpense);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<ExpenseResponse>(existingExpense);
        return response;
    }

    public async Task<DeleteExpenseResponse> DeleteExpense(DeleteExpenseRequest request)
    {
        try
        {
            var expense = await _context.Expenses.FindAsync(request.ExpenseId);
            if (expense == null)
            {
                return new DeleteExpenseResponse 
                { 
                    Success = false, 
                    Message = $"Expense with ID {request.ExpenseId} not found." 
                };
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            
            return new DeleteExpenseResponse 
            { 
                Success = true, 
                Message = "Expense deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteExpenseResponse 
            { 
                Success = false, 
                Message = $"Error deleting Expense: {ex.Message}" 
            };
        }
    }
}
