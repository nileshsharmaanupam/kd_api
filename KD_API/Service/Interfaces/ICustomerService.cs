using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface ICustomerService
{
    public Task<CustomerDTO> GetCustomerById(int customerId);
    public Task<IEnumerable<CustomerDTO>> GetAllCustomers();
    public Task<bool> CreateCustomer(CustomerDTO customerDto);
    public Task<CustomerDTO> UpdateCustomer(int customerId, CustomerDTO customerDto);
    public Task<bool> DeleteCustomer(int customerId);
}