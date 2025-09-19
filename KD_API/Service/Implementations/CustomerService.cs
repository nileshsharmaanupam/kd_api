using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class CustomerService : ICustomerService
{
    private readonly PostgresDbContext _context;

    public CustomerService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDTO> GetCustomerById(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            throw new ArgumentException($"Customer with ID {customerId} not found.");
        }
        return customer;
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<bool> CreateCustomer(CustomerDTO customerDto)
    {
        try
        {
            _context.Customers.Add(customerDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CustomerDTO> UpdateCustomer(int customerId, CustomerDTO customerDto)
    {
        var existingCustomer = await _context.Customers.FindAsync(customerId);
        if (existingCustomer == null)
        {
            throw new ArgumentException($"Customer with ID {customerId} not found.");
        }

        existingCustomer.Name = customerDto.Name;
        existingCustomer.Email = customerDto.Email;
        existingCustomer.Phone = customerDto.Phone;
        existingCustomer.Address = customerDto.Address;
        existingCustomer.Discription = customerDto.Discription;
        existingCustomer.JoinDate = customerDto.JoinDate;
        existingCustomer.IsActive = customerDto.IsActive;

        await _context.SaveChangesAsync();
        return existingCustomer;
    }

    public async Task<bool> DeleteCustomer(int customerId)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
