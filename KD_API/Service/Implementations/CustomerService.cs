using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Customer;
using KD_API.Models.APIResponse.Customer;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class CustomerService : ICustomerService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerResponse> GetCustomerById(GetCustomerByIdRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        if (customer == null)
        {
            throw new ArgumentException($"Customer with ID {request.CustomerId} not found.");
        }
        
        var response = _mapper.Map<CustomerResponse>(customer);
        return response;
    }

    public async Task<CustomerListResponse> GetAllCustomers(GetAllCustomersRequest request)
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<CustomerListResponse>(customers);
    }

    public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = _mapper.Map<CustomerDTO>(request);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CustomerResponse>(customer);
        return response;
    }

    public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
    {
        var existingCustomer = await _context.Customers.FindAsync(request.CustomerId);
        if (existingCustomer == null)
        {
            throw new ArgumentException($"Customer with ID {request.CustomerId} not found.");
        }
        
        _mapper.Map(request, existingCustomer);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CustomerResponse>(existingCustomer);
        return response;
    }

    public async Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                return new DeleteCustomerResponse 
                { 
                    Success = false, 
                    Message = $"Customer with ID {request.CustomerId} not found." 
                };
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return new DeleteCustomerResponse 
            { 
                Success = true, 
                Message = "Customer deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteCustomerResponse 
            { 
                Success = false, 
                Message = $"Error deleting Customer: {ex.Message}" 
            };
        }
    }
}
