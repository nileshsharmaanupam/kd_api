namespace KD_API.Models.APIRequests.Customer;

public class GetCustomerByIdRequest
{
    public int CustomerId { get; set; }
}

public class GetAllCustomersRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateCustomerRequest
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateCustomerRequest
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
}

public class DeleteCustomerRequest
{
    public int CustomerId { get; set; }
}

// Legacy classes for backward compatibility
public class CreateCustomer : CreateCustomerRequest { }
public class UpdateCustomer : UpdateCustomerRequest { }
