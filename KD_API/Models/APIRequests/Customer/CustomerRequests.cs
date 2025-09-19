namespace KD_API.Models.APIRequests.Customer;

public class CreateCustomer
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateCustomer
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
}
