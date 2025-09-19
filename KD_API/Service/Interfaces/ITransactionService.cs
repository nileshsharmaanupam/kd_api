using KD_API.Models.APIRequests.Transaction;
using KD_API.Models.APIResponse.Transaction;

namespace KD_API.Service.Interfaces;

public interface ITransactionService
{
    public Task<TransactionResponse> GetTransactionById(GetTransactionByIdRequest request);
    public Task<TransactionListResponse> GetAllTransactions(GetAllTransactionsRequest request);
    public Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request);
    public Task<TransactionResponse> UpdateTransaction(UpdateTransactionRequest request);
    public Task<DeleteTransactionResponse> DeleteTransaction(DeleteTransactionRequest request);
}
