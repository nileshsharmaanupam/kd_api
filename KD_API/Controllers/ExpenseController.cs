using KD_API.Models;
using KD_API.Models.APIRequests.Expense;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Expense;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController(IExpenseService expenseService) : ControllerBase
{
    [HttpGet("{expenseId}")]
    public async Task<IActionResult> GetExpenseById(int expenseId)
    {
        try
        {
            var expense = await expenseService.GetExpenseById(expenseId);
            var response = new ExpenseResponse
            {
                Id = expense.Id,
                Note = expense.Note,
                Amount = expense.Amount,
                Tag = expense.Tag,
                ExpenseDate = expense.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            return Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Expense retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            var expenses = await expenseService.GetAllExpenses();
            var expenseResponses = expenses.Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Note = e.Note,
                Amount = e.Amount,
                Tag = e.Tag,
                ExpenseDate = e.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            var listResponse = new ExpenseListResponse
            {
                Expenses = expenseResponses,
                TotalCount = expenseResponses.Count,
                TotalAmount = expenseResponses.Sum(e => e.Amount),
                AverageExpenseAmount = expenseResponses.Any() ? expenseResponses.Average(e => e.Amount) : 0,
                ExpensesByTag = expenseResponses.GroupBy(e => e.Tag?.ToString() ?? "No Tag")
                    .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount)),
                ExpenseCountByTag = expenseResponses.GroupBy(e => e.Tag?.ToString() ?? "No Tag")
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return Ok(new ApiResponse<ExpenseListResponse>
            {
                Success = true,
                Message = "Expenses retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<ExpenseListResponse>
            {
                Success = false,
                Message = "Failed to retrieve expenses",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpense request)
    {
        try
        {
            var expense = new Expense
            {
                Note = request.Note,
                Amount = request.Amount,
                Tag = request.Tag,
                ExpenseDate = request.ExpenseDate
            };

            bool result = await expenseService.CreateExpense(expense);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create expense",
                    Errors = new List<string> { "Expense creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Expense created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create expense",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{expenseId}")]
    public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateExpense request)
    {
        try
        {
            var expense = new Expense
            {
                Id = expenseId,
                Note = request.Note,
                Amount = request.Amount,
                Tag = request.Tag,
                ExpenseDate = request.ExpenseDate
            };

            var updatedExpense = await expenseService.UpdateExpense(expenseId, expense);
            var response = new ExpenseResponse
            {
                Id = updatedExpense.Id,
                Note = updatedExpense.Note,
                Amount = updatedExpense.Amount,
                Tag = updatedExpense.Tag,
                ExpenseDate = updatedExpense.ExpenseDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Expense updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            bool result = await expenseService.DeleteExpense(expenseId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Expense not found or failed to delete",
                    Errors = new List<string> { "Expense deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Expense deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete expense",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
