using KD_API.Models;
using KD_API.Models.APIRequests.CreditNote;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.CreditNote;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditNoteController(ICreditNoteService creditNoteService) : ControllerBase
{
    [HttpGet("{creditNoteId}")]
    public async Task<IActionResult> GetCreditNoteById(int creditNoteId)
    {
        try
        {
            var creditNote = await creditNoteService.GetCreditNoteById(creditNoteId);
            var response = new CreditNoteResponse
            {
                Id = creditNote.Id,
                CustomerId = creditNote.CustomerId,
                CustomerName = "Customer Name", // This would come from a join or separate service call
                CreditDate = creditNote.CreditDate,
                DueDate = creditNote.DueDate,
                SubTotal = creditNote.SubTotal,
                TaxAmount = creditNote.TaxAmount,
                TotalAmount = creditNote.TotalAmount,
                Status = creditNote.Status,
                Notes = creditNote.Notes,
                CreatedDate = creditNote.CreatedDate,
                CreatedAt = DateTime.UtcNow,
                IsOverdue = creditNote.DueDate < DateTime.Now,
                DaysUntilDue = (creditNote.DueDate - DateTime.Now).Days
            };

            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CreditNoteResponse>
            {
                Success = false,
                Message = "Failed to retrieve credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCreditNotes()
    {
        try
        {
            var creditNotes = await creditNoteService.GetAllCreditNotes();
            var creditNoteResponses = creditNotes.Select(c => new CreditNoteResponse
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                CustomerName = "Customer Name",
                CreditDate = c.CreditDate,
                DueDate = c.DueDate,
                SubTotal = c.SubTotal,
                TaxAmount = c.TaxAmount,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                Notes = c.Notes,
                CreatedDate = c.CreatedDate,
                CreatedAt = DateTime.UtcNow,
                IsOverdue = c.DueDate < DateTime.Now,
                DaysUntilDue = (c.DueDate - DateTime.Now).Days
            }).ToList();

            var listResponse = new CreditNoteListResponse
            {
                CreditNotes = creditNoteResponses,
                TotalCount = creditNoteResponses.Count,
                TotalAmount = creditNoteResponses.Sum(c => c.TotalAmount),
                PaidAmount = 0, // This would be calculated based on status
                PendingAmount = creditNoteResponses.Sum(c => c.TotalAmount),
                StatusCounts = creditNoteResponses.GroupBy(c => c.Status.ToString())
                    .ToDictionary(g => g.Key, g => g.Count()),
                OverdueCount = creditNoteResponses.Count(c => c.IsOverdue)
            };

            return Ok(new ApiResponse<CreditNoteListResponse>
            {
                Success = true,
                Message = "Credit notes retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CreditNoteListResponse>
            {
                Success = false,
                Message = "Failed to retrieve credit notes",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCreditNote([FromBody] CreateCreditNote request)
    {
        try
        {
            var creditNote = new CreditNote
            {
                CustomerId = request.CustomerId,
                CreditDate = request.CreditDate,
                DueDate = request.DueDate,
                SubTotal = request.SubTotal,
                TaxAmount = request.TaxAmount,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                Notes = request.Notes,
                CreatedDate = request.CreatedDate
            };

            bool result = await creditNoteService.CreateCreditNote(creditNote);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create credit note",
                    Errors = new List<string> { "Credit note creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Credit note created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{creditNoteId}")]
    public async Task<IActionResult> UpdateCreditNote(int creditNoteId, [FromBody] UpdateCreditNote request)
    {
        try
        {
            var creditNote = new CreditNote
            {
                Id = creditNoteId,
                CustomerId = request.CustomerId,
                CreditDate = request.CreditDate,
                DueDate = request.DueDate,
                SubTotal = request.SubTotal,
                TaxAmount = request.TaxAmount,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                Notes = request.Notes,
                CreatedDate = DateTime.UtcNow
            };

            var updatedCreditNote = await creditNoteService.UpdateCreditNote(creditNoteId, creditNote);
            var response = new CreditNoteResponse
            {
                Id = updatedCreditNote.Id,
                CustomerId = updatedCreditNote.CustomerId,
                CustomerName = "Customer Name",
                CreditDate = updatedCreditNote.CreditDate,
                DueDate = updatedCreditNote.DueDate,
                SubTotal = updatedCreditNote.SubTotal,
                TaxAmount = updatedCreditNote.TaxAmount,
                TotalAmount = updatedCreditNote.TotalAmount,
                Status = updatedCreditNote.Status,
                Notes = updatedCreditNote.Notes,
                CreatedDate = updatedCreditNote.CreatedDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsOverdue = updatedCreditNote.DueDate < DateTime.Now,
                DaysUntilDue = (updatedCreditNote.DueDate - DateTime.Now).Days
            };

            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CreditNoteResponse>
            {
                Success = false,
                Message = "Failed to update credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{creditNoteId}")]
    public async Task<IActionResult> DeleteCreditNote(int creditNoteId)
    {
        try
        {
            bool result = await creditNoteService.DeleteCreditNote(creditNoteId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Credit note not found or failed to delete",
                    Errors = new List<string> { "Credit note deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Credit note deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
