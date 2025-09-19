using KD_API.Models.APIRequests.Price;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Price;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PriceController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet("{priceId}")]
    public async Task<IActionResult> GetPriceById(int priceId)
    {
        try
        {
            var request = new GetPriceByIdRequest { PriceId = priceId };
            var response = await _priceService.GetPriceById(request);
            
            return Ok(new ApiResponse<PriceResponse>
            {
                Success = true,
                Message = "Price retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new GetAllPricesRequest();
            var response = await _priceService.GetAllPrices(request);
            
            return Ok(new ApiResponse<PriceListResponse>
            {
                Success = true,
                Message = "Prices retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<PriceListResponse>
            {
                Success = false,
                Message = "Failed to retrieve prices",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrice([FromBody] CreatePriceRequest request)
    {
        try
        {
            var response = await _priceService.CreatePrice(request);
            
            return Ok(new ApiResponse<PriceResponse>
            {
                Success = true,
                Message = "Price created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<PriceResponse>
            {
                Success = false,
                Message = "Failed to create price",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{priceId}")]
    public async Task<IActionResult> UpdatePrice(int priceId, [FromBody] UpdatePriceRequest request)
    {
        try
        {
            request.PriceId = priceId;
            var response = await _priceService.UpdatePrice(request);

            return Ok(new ApiResponse<PriceResponse>
            {
                Success = true,
                Message = "Price updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new DeletePriceRequest { PriceId = priceId };
            var response = await _priceService.DeletePrice(request);
            
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
                Message = "Failed to delete price",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
