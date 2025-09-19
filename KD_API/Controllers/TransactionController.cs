using KD_API.Models.APIRequests.Transaction;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Transaction;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionById(int transactionId)
    {
        try
        {
            var request = new GetTransactionByIdRequest { TransactionId = transactionId };
            var response = await _transactionService.GetTransactionById(request);
            
            return Ok(new ApiResponse<TransactionResponse>
            {
                Success = true,
                Message = "Transaction retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new GetAllTransactionsRequest();
            var response = await _transactionService.GetAllTransactions(request);
            
            return Ok(new ApiResponse<TransactionListResponse>
            {
                Success = true,
                Message = "Transactions retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<TransactionListResponse>
            {
                Success = false,
                Message = "Failed to retrieve transactions",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
    {
        try
        {
            var response = await _transactionService.CreateTransaction(request);
            
            return Ok(new ApiResponse<TransactionResponse>
            {
                Success = true,
                Message = "Transaction created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<TransactionResponse>
            {
                Success = false,
                Message = "Failed to create transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] UpdateTransactionRequest request)
    {
        try
        {
            request.TransactionId = transactionId;
            var response = await _transactionService.UpdateTransaction(request);

            return Ok(new ApiResponse<TransactionResponse>
            {
                Success = true,
                Message = "Transaction updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new DeleteTransactionRequest { TransactionId = transactionId };
            var response = await _transactionService.DeleteTransaction(request);
            
            if (!response.Success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = response.Message
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = response.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete transaction",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
