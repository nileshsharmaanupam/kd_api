using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Product;
using KD_API.Models.APIResponse.Product;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class ProductService : IProductService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductResponse> GetProductById(GetProductByIdRequest request)
    {
        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null)
        {
            throw new ArgumentException($"Product with ID {request.ProductId} not found.");
        }
        
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }

    public async Task<ProductListResponse> GetAllProducts(GetAllProductsRequest request)
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<ProductListResponse>(products);
    }

    public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        var product = _mapper.Map<ProductDTO>(request);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }

    public async Task<ProductResponse> UpdateProduct(UpdateProductRequest request)
    {
        var existingProduct = await _context.Products.FindAsync(request.ProductId);
        if (existingProduct == null)
        {
            throw new ArgumentException($"Product with ID {request.ProductId} not found.");
        }
        
        _mapper.Map(request, existingProduct);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<ProductResponse>(existingProduct);
        return response;
    }

    public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request)
    {
        try
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return new DeleteProductResponse 
                { 
                    Success = false, 
                    Message = $"Product with ID {request.ProductId} not found." 
                };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return new DeleteProductResponse 
            { 
                Success = true, 
                Message = "Product deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteProductResponse 
            { 
                Success = false, 
                Message = $"Error deleting Product: {ex.Message}" 
            };
        }
    }
}
