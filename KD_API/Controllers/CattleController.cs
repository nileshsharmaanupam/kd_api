using KD_API.Models;
using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CattleController : ControllerBase
{
    private readonly ICattleService _cattleService;
    private readonly IMapper _mapper;

    public CattleController(ICattleService cattleService, IMapper mapper)
    {
        _cattleService = cattleService;
        _mapper = mapper;
    }

    [HttpGet("{cattleId}")]
    public async Task<IActionResult> GetCattleById(int cattleId)
    {
        try
        {
            var cattleResponse = await _cattleService.GetCattleById(cattleId);
            
            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle retrieved successfully",
                Data = cattleResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CattleResponse>
            {
                Success = false,
                Message = "Failed to retrieve cattle",
                Data = null
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCattle()
    {
        try
        {
            var cattleListResponse = await _cattleService.GetAllCattle();
            
            return Ok(new ApiResponse<CattleListResponse>
            {
                Success = true,
                Message = "All cattle retrieved successfully",
                Data = cattleListResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CattleListResponse>
            {
                Success = false,
                Message = "Failed to retrieve cattle list",
                Data = null
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCattle([FromBody] CreateCattle request)
    {
        try
        {
            var cattle = _mapper.Map<CattleDTO>(request);
            bool result = await _cattleService.CreateCattle(cattle);
            
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create cattle",
                    Errors = new List<string> { "Cattle creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cattle created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{cattleId}")]
    public async Task<IActionResult> UpdateCattle(int cattleId, [FromBody] UpdateCattle request)
    {
        try
        {
            var cattle = _mapper.Map<CattleDTO>(request);
            cattle.Id = cattleId;
            
            var updatedCattle = await _cattleService.UpdateCattle(cattleId, cattle);
            var response = _mapper.Map<CattleResponse>(updatedCattle);
            response.UpdatedAt = DateTime.UtcNow;

            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            bool result = await _cattleService.DeleteCattle(cattleId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Cattle not found or failed to delete",
                    Errors = new List<string> { "Cattle deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cattle deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
