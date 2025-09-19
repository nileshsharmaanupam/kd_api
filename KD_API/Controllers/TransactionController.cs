using KD_API.Models;
using KD_API.Models.APIRequests.Transaction;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Transaction;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionById(int transactionId)
    {
        try
        {
            var transaction = await transactionService.GetTransactionById(transactionId);
            var response = new TransactionResponse
            {
                Id = transaction.Id,
                CustomerId = transaction.CustomerId,
                CustomerName = "Customer Name", // This would come from a join or separate service call
                CattleId = transaction.CattleId,
                CattleName = transaction.CattleId.HasValue ? "Cattle Name" : null,
                ProductId = transaction.ProductId,
                ProductName = transaction.ProductId.HasValue ? "Product Name" : null,
                Price = transaction.Price,
                Amount = transaction.Amount,
                Quantity = transaction.Quantity,
                TransactionDate = transaction.TransactionDate,
                Status = transaction.Status,
                CreatedAt = DateTime.UtcNow
            };

            return Ok(new ApiResponse<TransactionResponse>
            {
                Success = true,
                Message = "Transaction retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<TransactionResponse>
            {
                Success = false,
                Message = "Failed to retrieve transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        try
        {
            var transactions = await transactionService.GetAllTransactions();
            var transactionResponses = transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,
                CustomerId = t.CustomerId,
                CustomerName = "Customer Name",
                CattleId = t.CattleId,
                CattleName = t.CattleId.HasValue ? "Cattle Name" : null,
                ProductId = t.ProductId,
                ProductName = t.ProductId.HasValue ? "Product Name" : null,
                Price = t.Price,
                Amount = t.Amount,
                Quantity = t.Quantity,
                TransactionDate = t.TransactionDate,
                Status = t.Status,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            var listResponse = new TransactionListResponse
            {
                Transactions = transactionResponses,
                TotalCount = transactionResponses.Count,
                TotalAmount = transactionResponses.Sum(t => t.Amount),
                AverageTransactionAmount = transactionResponses.Any() ? transactionResponses.Average(t => t.Amount) : 0,
                StatusCounts = transactionResponses.GroupBy(t => t.Status)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return Ok(new ApiResponse<TransactionListResponse>
            {
                Success = true,
                Message = "Transactions retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<TransactionListResponse>
            {
                Success = false,
                Message = "Failed to retrieve transactions",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransaction request)
    {
        try
        {
            var transaction = new Transaction
            {
                CustomerId = request.CustomerId,
                CattleId = request.CattleId,
                ProductId = request.ProductId,
                Price = request.Price,
                Amount = request.Amount,
                Quantity = request.Quantity,
                TransactionDate = request.TransactionDate,
                Status = request.Status
            };

            bool result = await transactionService.CreateTransaction(transaction);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create transaction",
                    Errors = new List<string> { "Transaction creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Transaction created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] UpdateTransaction request)
    {
        try
        {
            var transaction = new Transaction
            {
                Id = transactionId,
                CustomerId = request.CustomerId,
                CattleId = request.CattleId,
                ProductId = request.ProductId,
                Price = request.Price,
                Amount = request.Amount,
                Quantity = request.Quantity,
                TransactionDate = request.TransactionDate,
                Status = request.Status
            };

            var updatedTransaction = await transactionService.UpdateTransaction(transactionId, transaction);
            var response = new TransactionResponse
            {
                Id = updatedTransaction.Id,
                CustomerId = updatedTransaction.CustomerId,
                CustomerName = "Customer Name",
                CattleId = updatedTransaction.CattleId,
                CattleName = updatedTransaction.CattleId.HasValue ? "Cattle Name" : null,
                ProductId = updatedTransaction.ProductId,
                ProductName = updatedTransaction.ProductId.HasValue ? "Product Name" : null,
                Price = updatedTransaction.Price,
                Amount = updatedTransaction.Amount,
                Quantity = updatedTransaction.Quantity,
                TransactionDate = updatedTransaction.TransactionDate,
                Status = updatedTransaction.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return Ok(new ApiResponse<TransactionResponse>
            {
                Success = true,
                Message = "Transaction updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<TransactionResponse>
            {
                Success = false,
                Message = "Failed to update transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> DeleteTransaction(int transactionId)
    {
        try
        {
            bool result = await transactionService.DeleteTransaction(transactionId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Transaction not found or failed to delete",
                    Errors = new List<string> { "Transaction deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Transaction deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
