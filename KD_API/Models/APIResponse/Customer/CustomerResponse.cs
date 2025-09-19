namespace KD_API.Models.APIResponse.Customer;

public class CustomerResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int TotalTransactions { get; set; }
    public decimal TotalSpent { get; set; }
}

public class CustomerListResponse
{
    public List<CustomerResponse> Customers { get; set; }
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int InactiveCount { get; set; }
}
