using KD_API.Models.APIRequests.CreditNote;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.CreditNote;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditNoteController : ControllerBase
{
    private readonly ICreditNoteService _creditNoteService;

    public CreditNoteController(ICreditNoteService creditNoteService)
    {
        _creditNoteService = creditNoteService;
    }

    [HttpGet("{creditNoteId}")]
    public async Task<IActionResult> GetCreditNoteById(int creditNoteId)
    {
        try
        {
            var request = new GetCreditNoteByIdRequest { CreditNoteId = creditNoteId };
            var response = await _creditNoteService.GetCreditNoteById(request);
            
            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new GetAllCreditNotesRequest();
            var response = await _creditNoteService.GetAllCreditNotes(request);
            
            return Ok(new ApiResponse<CreditNoteListResponse>
            {
                Success = true,
                Message = "Credit notes retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CreditNoteListResponse>
            {
                Success = false,
                Message = "Failed to retrieve credit notes",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCreditNote([FromBody] CreateCreditNoteRequest request)
    {
        try
        {
            var response = await _creditNoteService.CreateCreditNote(request);
            
            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CreditNoteResponse>
            {
                Success = false,
                Message = "Failed to create credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{creditNoteId}")]
    public async Task<IActionResult> UpdateCreditNote(int creditNoteId, [FromBody] UpdateCreditNoteRequest request)
    {
        try
        {
            request.CreditNoteId = creditNoteId;
            var response = await _creditNoteService.UpdateCreditNote(request);

            return Ok(new ApiResponse<CreditNoteResponse>
            {
                Success = true,
                Message = "Credit note updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new DeleteCreditNoteRequest { CreditNoteId = creditNoteId };
            var response = await _creditNoteService.DeleteCreditNote(request);
            
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
                Message = "Failed to delete credit note",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
