using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class ProductService : IProductService
{
    private readonly PostgresDbContext _context;

    public ProductService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDTO> GetProductById(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new ArgumentException($"Product with ID {productId} not found.");
        }
        return product;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<bool> CreateProduct(ProductDTO productDto)
    {
        try
        {
            _context.Products.Add(productDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDto)
    {
        var existingProduct = await _context.Products.FindAsync(productId);
        if (existingProduct == null)
        {
            throw new ArgumentException($"Product with ID {productId} not found.");
        }

        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.Price = productDto.Price;
        existingProduct.CreatedDate = productDto.CreatedDate;
        existingProduct.IsActive = productDto.IsActive;

        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
