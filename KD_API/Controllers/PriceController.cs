using KD_API.Models;
using KD_API.Models.APIRequests.Price;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Price;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController(IPriceService priceService) : ControllerBase
{
    [HttpGet("{priceId}")]
    public async Task<IActionResult> GetPriceById(int priceId)
    {
        try
        {
            var price = await priceService.GetPriceById(priceId);
            var response = new PriceResponse
            {
                Id = price.Id,
                ProductId = price.ProductId,
                ProductName = price.ProductId.HasValue ? "Product Name" : null,
                CattleId = price.CattleId,
                CattleName = price.CattleId.HasValue ? "Cattle Name" : null,
                PriceValue = price.PriceValue,
                PriceType = price.PriceType,
                EffectiveDate = price.EffectiveDate,
                ExpiryDate = price.ExpiryDate,
                Currency = price.Currency,
                Notes = price.Notes,
                IsActive = price.IsActive,
                CreatedAt = DateTime.UtcNow,
                IsExpired = price.ExpiryDate.HasValue && price.ExpiryDate < DateTime.Now,
                DaysUntilExpiry = price.ExpiryDate.HasValue ? (price.ExpiryDate.Value - DateTime.Now).Days : 0
            };

            return Ok(new ApiResponse<PriceResponse>
            {
                Success = true,
                Message = "Price retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<PriceResponse>
            {
                Success = false,
                Message = "Failed to retrieve price",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrices()
    {
        try
        {
            var prices = await priceService.GetAllPrices();
            var priceResponses = prices.Select(p => new PriceResponse
            {
                Id = p.Id,
                ProductId = p.ProductId,
                ProductName = p.ProductId.HasValue ? "Product Name" : null,
                CattleId = p.CattleId,
                CattleName = p.CattleId.HasValue ? "Cattle Name" : null,
                PriceValue = p.PriceValue,
                PriceType = p.PriceType,
                EffectiveDate = p.EffectiveDate,
                ExpiryDate = p.ExpiryDate,
                Currency = p.Currency,
                Notes = p.Notes,
                IsActive = p.IsActive,
                CreatedAt = DateTime.UtcNow,
                IsExpired = p.ExpiryDate.HasValue && p.ExpiryDate < DateTime.Now,
                DaysUntilExpiry = p.ExpiryDate.HasValue ? (p.ExpiryDate.Value - DateTime.Now).Days : 0
            }).ToList();

            var listResponse = new PriceListResponse
            {
                Prices = priceResponses,
                TotalCount = priceResponses.Count,
                ActiveCount = priceResponses.Count(p => p.IsActive),
                ExpiredCount = priceResponses.Count(p => p.IsExpired),
                AveragePriceByType = priceResponses.GroupBy(p => p.PriceType)
                    .ToDictionary(g => g.Key, g => g.Average(p => p.PriceValue)),
                PriceCountByType = priceResponses.GroupBy(p => p.PriceType)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return Ok(new ApiResponse<PriceListResponse>
            {
                Success = true,
                Message = "Prices retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<PriceListResponse>
            {
                Success = false,
                Message = "Failed to retrieve prices",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrice([FromBody] CreatePrice request)
    {
        try
        {
            var price = new Price
            {
                ProductId = request.ProductId,
                CattleId = request.CattleId,
                PriceValue = request.PriceValue,
                PriceType = request.PriceType,
                EffectiveDate = request.EffectiveDate,
                ExpiryDate = request.ExpiryDate,
                Currency = request.Currency,
                Notes = request.Notes,
                IsActive = request.IsActive
            };

            bool result = await priceService.CreatePrice(price);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create price",
                    Errors = new List<string> { "Price creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Price created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create price",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{priceId}")]
    public async Task<IActionResult> UpdatePrice(int priceId, [FromBody] UpdatePrice request)
    {
        try
        {
            var price = new Price
            {
                Id = priceId,
                ProductId = request.ProductId,
                CattleId = request.CattleId,
                PriceValue = request.PriceValue,
                PriceType = request.PriceType,
                EffectiveDate = request.EffectiveDate,
                ExpiryDate = request.ExpiryDate,
                Currency = request.Currency,
                Notes = request.Notes,
                IsActive = request.IsActive
            };

            var updatedPrice = await priceService.UpdatePrice(priceId, price);
            var response = new PriceResponse
            {
                Id = updatedPrice.Id,
                ProductId = updatedPrice.ProductId,
                ProductName = updatedPrice.ProductId.HasValue ? "Product Name" : null,
                CattleId = updatedPrice.CattleId,
                CattleName = updatedPrice.CattleId.HasValue ? "Cattle Name" : null,
                PriceValue = updatedPrice.PriceValue,
                PriceType = updatedPrice.PriceType,
                EffectiveDate = updatedPrice.EffectiveDate,
                ExpiryDate = updatedPrice.ExpiryDate,
                Currency = updatedPrice.Currency,
                Notes = updatedPrice.Notes,
                IsActive = updatedPrice.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsExpired = updatedPrice.ExpiryDate.HasValue && updatedPrice.ExpiryDate < DateTime.Now,
                DaysUntilExpiry = updatedPrice.ExpiryDate.HasValue ? (updatedPrice.ExpiryDate.Value - DateTime.Now).Days : 0
            };

            return Ok(new ApiResponse<PriceResponse>
            {
                Success = true,
                Message = "Price updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<PriceResponse>
            {
                Success = false,
                Message = "Failed to update price",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{priceId}")]
    public async Task<IActionResult> DeletePrice(int priceId)
    {
        try
        {
            bool result = await priceService.DeletePrice(priceId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Price not found or failed to delete",
                    Errors = new List<string> { "Price deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Price deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete price",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
