using KD_API.Models.APIRequests.Product;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Product;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        try
        {
            var request = new GetProductByIdRequest { ProductId = productId };
            var response = await _productService.GetProductById(request);
            
            return Ok(new ApiResponse<ProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ProductResponse>
            {
                Success = false,
                Message = "Failed to retrieve product",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var request = new GetAllProductsRequest();
            var response = await _productService.GetAllProducts(request);
            
            return Ok(new ApiResponse<ProductListResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ProductListResponse>
            {
                Success = false,
                Message = "Failed to retrieve products",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        try
        {
            var response = await _productService.CreateProduct(request);
            
            return Ok(new ApiResponse<ProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ProductResponse>
            {
                Success = false,
                Message = "Failed to create product",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductRequest request)
    {
        try
        {
            request.ProductId = productId;
            var response = await _productService.UpdateProduct(request);

            return Ok(new ApiResponse<ProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<ProductResponse>
            {
                Success = false,
                Message = "Failed to update product",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        try
        {
            var request = new DeleteProductRequest { ProductId = productId };
            var response = await _productService.DeleteProduct(request);
            
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
                Message = "Failed to delete product",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
