using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface IProductService
{
    public Task<ProductDTO> GetProductById(int productId);
    public Task<IEnumerable<ProductDTO>> GetAllProducts();
    public Task<bool> CreateProduct(ProductDTO productDto);
    public Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDto);
    public Task<bool> DeleteProduct(int productId);
}
