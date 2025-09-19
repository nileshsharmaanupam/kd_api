using KD_API.Models;
using KD_API.Models.APIRequests.Product;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Product;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        try
        {
            var product = await productService.GetProductById(productId);
            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
                IsActive = product.IsActive,
                CreatedAt = DateTime.UtcNow,
                TotalSales = 0,
                TotalRevenue = 0
            };

            return Ok(new ApiResponse<ProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            var products = await productService.GetAllProducts();
            var productResponses = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedDate = p.CreatedDate,
                IsActive = p.IsActive,
                CreatedAt = DateTime.UtcNow,
                TotalSales = 0,
                TotalRevenue = 0
            }).ToList();

            var listResponse = new ProductListResponse
            {
                Products = productResponses,
                TotalCount = productResponses.Count,
                ActiveCount = productResponses.Count(p => p.IsActive),
                InactiveCount = productResponses.Count(p => !p.IsActive),
                AveragePrice = productResponses.Any() ? productResponses.Average(p => p.Price) : 0
            };

            return Ok(new ApiResponse<ProductListResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<ProductListResponse>
            {
                Success = false,
                Message = "Failed to retrieve products",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProduct request)
    {
        try
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedDate = request.CreatedDate,
                IsActive = request.IsActive
            };

            bool result = await productService.CreateProduct(product);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create product",
                    Errors = new List<string> { "Product creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Product created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create product",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProduct request)
    {
        try
        {
            var product = new Product
            {
                Id = productId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedDate = request.CreatedDate,
                IsActive = request.IsActive
            };

            var updatedProduct = await productService.UpdateProduct(productId, product);
            var response = new ProductResponse
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                CreatedDate = updatedProduct.CreatedDate,
                IsActive = updatedProduct.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalSales = 0,
                TotalRevenue = 0
            };

            return Ok(new ApiResponse<ProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            bool result = await productService.DeleteProduct(productId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Product not found or failed to delete",
                    Errors = new List<string> { "Product deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete product",
                Errors = new List<string> { e.Message }
            });
        }
    }
}
