using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Transaction;
using KD_API.Models.APIResponse.Transaction;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class TransactionService : ITransactionService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public TransactionService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> GetTransactionById(GetTransactionByIdRequest request)
    {
        var transaction = await _context.Transactions.FindAsync(request.TransactionId);
        if (transaction == null)
        {
            throw new ArgumentException($"Transaction with ID {request.TransactionId} not found.");
        }
        
        var response = _mapper.Map<TransactionResponse>(transaction);
        return response;
    }

    public async Task<TransactionListResponse> GetAllTransactions(GetAllTransactionsRequest request)
    {
        var transactions = await _context.Transactions.ToListAsync();
        return _mapper.Map<TransactionListResponse>(transactions);
    }

    public async Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request)
    {
        var transaction = _mapper.Map<TransactionDTO>(request);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<TransactionResponse>(transaction);
        return response;
    }

    public async Task<TransactionResponse> UpdateTransaction(UpdateTransactionRequest request)
    {
        var existingTransaction = await _context.Transactions.FindAsync(request.TransactionId);
        if (existingTransaction == null)
        {
            throw new ArgumentException($"Transaction with ID {request.TransactionId} not found.");
        }
        
        _mapper.Map(request, existingTransaction);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<TransactionResponse>(existingTransaction);
        return response;
    }

    public async Task<DeleteTransactionResponse> DeleteTransaction(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await _context.Transactions.FindAsync(request.TransactionId);
            if (transaction == null)
            {
                return new DeleteTransactionResponse 
                { 
                    Success = false, 
                    Message = $"Transaction with ID {request.TransactionId} not found." 
                };
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            
            return new DeleteTransactionResponse 
            { 
                Success = true, 
                Message = "Transaction deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteTransactionResponse 
            { 
                Success = false, 
                Message = $"Error deleting Transaction: {ex.Message}" 
            };
        }
    }
}
