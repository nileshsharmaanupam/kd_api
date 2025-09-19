using KD_API.Models.APIRequests.Product;
using KD_API.Models.APIResponse.Product;

namespace KD_API.Service.Interfaces;

public interface IProductService
{
    public Task<ProductResponse> GetProductById(GetProductByIdRequest request);
    public Task<ProductListResponse> GetAllProducts(GetAllProductsRequest request);
    public Task<ProductResponse> CreateProduct(CreateProductRequest request);
    public Task<ProductResponse> UpdateProduct(UpdateProductRequest request);
    public Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request);
}
