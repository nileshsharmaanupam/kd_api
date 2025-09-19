using KD_API.Models.APIRequests.Customer;
using KD_API.Models.APIResponse.Customer;

namespace KD_API.Service.Interfaces;

public interface ICustomerService
{
    public Task<CustomerResponse> GetCustomerById(GetCustomerByIdRequest request);
    public Task<CustomerListResponse> GetAllCustomers(GetAllCustomersRequest request);
    public Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request);
    public Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request);
    public Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request);
}