using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class ExpenseService : IExpenseService
{
    private readonly PostgresDbContext _context;

    public ExpenseService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseDTO> GetExpenseById(int expenseId)
    {
        var expense = await _context.Expenses
            .Include(e => e.Tag)
            .FirstOrDefaultAsync(e => e.Id == expenseId);
        if (expense == null)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not found.");
        }
        return expense;
    }

    public async Task<IEnumerable<ExpenseDTO>> GetAllExpenses()
    {
        return await _context.Expenses
            .Include(e => e.Tag)
            .ToListAsync();
    }

    public async Task<bool> CreateExpense(ExpenseDTO expenseDto)
    {
        try
        {
            _context.Expenses.Add(expenseDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ExpenseDTO> UpdateExpense(int expenseId, ExpenseDTO expenseDto)
    {
        var existingExpense = await _context.Expenses.FindAsync(expenseId);
        if (existingExpense == null)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not found.");
        }

        existingExpense.Note = expenseDto.Note;
        existingExpense.Amount = expenseDto.Amount;
        existingExpense.Tag = expenseDto.Tag;
        existingExpense.ExpenseDate = expenseDto.ExpenseDate;

        await _context.SaveChangesAsync();
        return existingExpense;
    }

    public async Task<bool> DeleteExpense(int expenseId)
    {
        try
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense == null)
            {
                return false;
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
