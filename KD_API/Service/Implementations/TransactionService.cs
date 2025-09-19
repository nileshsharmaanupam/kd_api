using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class TransactionService : ITransactionService
{
    private readonly PostgresDbContext _context;

    public TransactionService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDTO> GetTransactionById(int transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction == null)
        {
            throw new ArgumentException($"Transaction with ID {transactionId} not found.");
        }
        return transaction;
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllTransactions()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<bool> CreateTransaction(TransactionDTO transactionDto)
    {
        try
        {
            _context.Transactions.Add(transactionDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<TransactionDTO> UpdateTransaction(int transactionId, TransactionDTO transactionDto)
    {
        var existingTransaction = await _context.Transactions.FindAsync(transactionId);
        if (existingTransaction == null)
        {
            throw new ArgumentException($"Transaction with ID {transactionId} not found.");
        }

        existingTransaction.CustomerId = transactionDto.CustomerId;
        existingTransaction.CattleId = transactionDto.CattleId;
        existingTransaction.ProductId = transactionDto.ProductId;
        existingTransaction.Price = transactionDto.Price;
        existingTransaction.Amount = transactionDto.Amount;
        existingTransaction.Quantity = transactionDto.Quantity;
        existingTransaction.TransactionDate = transactionDto.TransactionDate;
        existingTransaction.Status = transactionDto.Status;

        await _context.SaveChangesAsync();
        return existingTransaction;
    }

    public async Task<bool> DeleteTransaction(int transactionId)
    {
        try
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null)
            {
                return false;
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
