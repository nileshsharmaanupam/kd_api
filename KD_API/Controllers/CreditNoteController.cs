using KD_API.Models;
using KD_API.Models.APIRequests.CreditNote;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.CreditNote;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditNoteController : ControllerBase
{
    private readonly ICreditNoteService _creditNoteService;
    private readonly IMapper _mapper;

    public CreditNoteController(ICreditNoteService creditNoteService, IMapper mapper)
    {
        _creditNoteService = creditNoteService;
        _mapper = mapper;
    }

    [HttpGet("{creditNoteId}")]
    public async Task<IActionResult> GetCreditNoteById(int creditNoteId)
    {
        try
        {
            var creditNote = await _creditNoteService.GetCreditNoteById(creditNoteId);
            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note retrieved successfully",
                Data = creditNote
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
            var creditNoteListResponse = await _creditNoteService.GetAllCreditNotes();
            
            return Ok(new ApiResponse<CreditNoteListResponse>
            {
                Success = true,
                Message = "Credit notes retrieved successfully",
                Data = creditNoteListResponse
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
            var creditNote = _mapper.Map<CreditNoteDTO>(request);
            bool result = await _creditNoteService.CreateCreditNote(creditNote);
            
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
            var creditNote = _mapper.Map<CreditNoteDTO>(request);
            creditNote.Id = creditNoteId;
            
            var updatedCreditNote = await _creditNoteService.UpdateCreditNote(creditNoteId, creditNote);
            var response = _mapper.Map<CreditNoteResponse>(updatedCreditNote);
            response.UpdatedAt = DateTime.UtcNow;

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
            bool result = await _creditNoteService.DeleteCreditNote(creditNoteId);
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
