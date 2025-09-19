namespace KD_API.Models.APIRequests.Product;

public class GetProductByIdRequest
{
    public int ProductId { get; set; }
}

public class GetAllProductsRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateProductRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateProductRequest
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

public class DeleteProductRequest
{
    public int ProductId { get; set; }
}

// Legacy classes for backward compatibility
public class CreateProduct : CreateProductRequest { }
public class UpdateProduct : UpdateProductRequest { }
