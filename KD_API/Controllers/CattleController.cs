using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CattleController : ControllerBase
{
    private readonly ICattleService _cattleService;

    public CattleController(ICattleService cattleService)
    {
        _cattleService = cattleService;
    }

    [HttpGet("{cattleId}")]
    public async Task<IActionResult> GetCattleById(int cattleId)
    {
        try
        {
            var request = new GetCattleByIdRequest { CattleId = cattleId };
            var response = await _cattleService.GetCattleById(request);
            
            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CattleResponse>
            {
                Success = false,
                Message = "Failed to retrieve cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCattle()
    {
        try
        {
            var request = new GetAllCattleRequest();
            var response = await _cattleService.GetAllCattle(request);
            
            return Ok(new ApiResponse<CattleListResponse>
            {
                Success = true,
                Message = "Cattle retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CattleListResponse>
            {
                Success = false,
                Message = "Failed to retrieve cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCattle([FromBody] CreateCattleRequest request)
    {
        try
        {
            var response = await _cattleService.CreateCattle(request);
            
            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CattleResponse>
            {
                Success = false,
                Message = "Failed to create cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{cattleId}")]
    public async Task<IActionResult> UpdateCattle(int cattleId, [FromBody] UpdateCattleRequest request)
    {
        try
        {
            request.CattleId = cattleId;
            var response = await _cattleService.UpdateCattle(request);

            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CattleResponse>
            {
                Success = false,
                Message = "Failed to update cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{cattleId}")]
    public async Task<IActionResult> DeleteCattle(int cattleId)
    {
        try
        {
            var request = new DeleteCattleRequest { CattleId = cattleId };
            var response = await _cattleService.DeleteCattle(request);
            
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
                Message = "Failed to delete cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
