namespace KD_API.Models.APIResponse.Product;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int TotalSales { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class ProductListResponse
{
    public List<ProductResponse> Products { get; set; }
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int InactiveCount { get; set; }
    public decimal AveragePrice { get; set; }
}
