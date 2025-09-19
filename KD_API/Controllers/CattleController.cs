using KD_API.Models;
using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CattleController(ICattleService cattleService) : ControllerBase
{
    [HttpGet("{cattleId}")]
    public async Task<IActionResult> GetCattleById(int cattleId)
    {
        try
        {
            var cattle = await cattleService.GetCattleById(cattleId);
            var response = new CattleResponse
            {
                Id = cattle.Id,
                Name = cattle.Name,
                Breed = cattle.Breed,
                BirthDate = cattle.BirthDate,
                Weight = cattle.Weight,
                Gender = cattle.Gender,
                Color = cattle.Color,
                HealthStatus = cattle.HealthStatus,
                PurchaseDate = cattle.PurchaseDate,
                PurchasePrice = cattle.PurchasePrice,
                IsActive = cattle.IsActive,
                LastBreedingDate = cattle.LastBreedingDate,
                isLactating = cattle.isLactating,
                CreatedAt = DateTime.UtcNow
            };

            return Ok(new ApiResponse<CattleResponse>
            {
                Success = true,
                Message = "Cattle retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            var cattleList = await cattleService.GetAllCattle();
            var cattleResponses = cattleList.Select(c => new CattleResponse
            {
                Id = c.Id,
                Name = c.Name,
                Breed = c.Breed,
                BirthDate = c.BirthDate,
                Weight = c.Weight,
                Gender = c.Gender,
                Color = c.Color,
                HealthStatus = c.HealthStatus,
                PurchaseDate = c.PurchaseDate,
                PurchasePrice = c.PurchasePrice,
                IsActive = c.IsActive,
                LastBreedingDate = c.LastBreedingDate,
                isLactating = c.isLactating,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            var listResponse = new CattleListResponse
            {
                Cattle = cattleResponses,
                TotalCount = cattleResponses.Count,
                ActiveCount = cattleResponses.Count(c => c.IsActive),
                InactiveCount = cattleResponses.Count(c => !c.IsActive)
            };

            return Ok(new ApiResponse<CattleListResponse>
            {
                Success = true,
                Message = "Cattle retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CattleListResponse>
            {
                Success = false,
                Message = "Failed to retrieve cattle",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCattle([FromBody] CreateCattle request)
    {
        try
        {
            var cattle = new Cattle
            {
                Name = request.Name,
                Breed = request.Breed,
                BirthDate = request.BirthDate,
                Weight = request.Weight,
                Gender = request.Gender,
                Color = request.Color,
                HealthStatus = request.HealthStatus,
                PurchaseDate = request.PurchaseDate,
                PurchasePrice = request.PurchasePrice,
                LastBreedingDate = request.LastBreedingDate,
                isLactating = request.isLactating,
                IsActive = true
            };

            bool result = await cattleService.CreateCattle(cattle);
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
            var cattle = new Cattle
            {
                Id = cattleId,
                Name = request.Name,
                Breed = request.Breed,
                BirthDate = request.BirthDate,
                Weight = request.Weight,
                Gender = request.Gender,
                Color = request.Color,
                HealthStatus = request.HealthStatus,
                PurchaseDate = request.PurchaseDate,
                PurchasePrice = request.PurchasePrice,
                LastBreedingDate = request.LastBreedingDate,
                isLactating = request.isLactating,
                IsActive = true
            };

            var updatedCattle = await cattleService.UpdateCattle(cattleId, cattle);
            var response = new CattleResponse
            {
                Id = updatedCattle.Id,
                Name = updatedCattle.Name,
                Breed = updatedCattle.Breed,
                BirthDate = updatedCattle.BirthDate,
                Weight = updatedCattle.Weight,
                Gender = updatedCattle.Gender,
                Color = updatedCattle.Color,
                HealthStatus = updatedCattle.HealthStatus,
                PurchaseDate = updatedCattle.PurchaseDate,
                PurchasePrice = updatedCattle.PurchasePrice,
                IsActive = updatedCattle.IsActive,
                LastBreedingDate = updatedCattle.LastBreedingDate,
                isLactating = updatedCattle.isLactating,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

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
            bool result = await cattleService.DeleteCattle(cattleId);
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
