using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface ITransactionService
{
    public Task<TransactionDTO> GetTransactionById(int transactionId);
    public Task<IEnumerable<TransactionDTO>> GetAllTransactions();
    public Task<bool> CreateTransaction(TransactionDTO transactionDto);
    public Task<TransactionDTO> UpdateTransaction(int transactionId, TransactionDTO transactionDto);
    public Task<bool> DeleteTransaction(int transactionId);
}
