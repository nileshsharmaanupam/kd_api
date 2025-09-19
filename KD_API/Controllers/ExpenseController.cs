using KD_API.Models.APIRequests.Expense;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Expense;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("{expenseId}")]
    public async Task<IActionResult> GetExpenseById(int expenseId)
    {
        try
        {
            var request = new GetExpenseByIdRequest { ExpenseId = expenseId };
            var response = await _expenseService.GetExpenseById(request);
            
            return Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Expense retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ExpenseResponse>
            {
                Success = false,
                Message = "Failed to retrieve expense",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExpenses()
    {
        try
        {
            var request = new GetAllExpensesRequest();
            var response = await _expenseService.GetAllExpenses(request);
            
            return Ok(new ApiResponse<ExpenseListResponse>
            {
                Success = true,
                Message = "Expenses retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ExpenseListResponse>
            {
                Success = false,
                Message = "Failed to retrieve expenses",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        try
        {
            var response = await _expenseService.CreateExpense(request);
            
            return Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Expense created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ExpenseResponse>
            {
                Success = false,
                Message = "Failed to create expense",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{expenseId}")]
    public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateExpenseRequest request)
    {
        try
        {
            request.ExpenseId = expenseId;
            var response = await _expenseService.UpdateExpense(request);

            return Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Expense updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ExpenseResponse>
            {
                Success = false,
                Message = "Failed to update expense",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{expenseId}")]
    public async Task<IActionResult> DeleteExpense(int expenseId)
    {
        try
        {
            var request = new DeleteExpenseRequest { ExpenseId = expenseId };
            var response = await _expenseService.DeleteExpense(request);
            
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
                Message = "Failed to delete expense",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
