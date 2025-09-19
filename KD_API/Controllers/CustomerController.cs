using KD_API.Models;
using KD_API.Models.APIRequests.Customer;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Customer;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        try
        {
            var customer = await customerService.GetCustomerById(customerId);
            var response = new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Discription = customer.Discription,
                JoinDate = customer.JoinDate,
                IsActive = customer.IsActive,
                CreatedAt = DateTime.UtcNow, // You may want to add these fields to your model
                TotalTransactions = 0, // This would come from a service method
                TotalSpent = 0 // This would come from a service method
            };

            return Ok(new ApiResponse<CustomerResponse>
            {
                Success = true,
                Message = "Customer retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CustomerResponse>
            {
                Success = false,
                Message = "Failed to retrieve customer",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customers = await customerService.GetAllCustomers();
            var customerResponses = customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Discription = c.Discription,
                JoinDate = c.JoinDate,
                IsActive = c.IsActive,
                CreatedAt = DateTime.UtcNow,
                TotalTransactions = 0,
                TotalSpent = 0
            }).ToList();

            var listResponse = new CustomerListResponse
            {
                Customers = customerResponses,
                TotalCount = customerResponses.Count,
                ActiveCount = customerResponses.Count(c => c.IsActive),
                InactiveCount = customerResponses.Count(c => !c.IsActive)
            };

            return Ok(new ApiResponse<CustomerListResponse>
            {
                Success = true,
                Message = "Customers retrieved successfully",
                Data = listResponse
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CustomerListResponse>
            {
                Success = false,
                Message = "Failed to retrieve customers",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer request)
    {
        try
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                Discription = request.Discription,
                JoinDate = request.JoinDate,
                IsActive = request.IsActive
            };

            bool result = await customerService.CreateCustomer(customer);
            if (!result)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Failed to create customer",
                    Errors = new List<string> { "Customer creation failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Customer created successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to create customer",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomer request)
    {
        try
        {
            var customer = new Customer
            {
                Id = customerId,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                Discription = request.Discription,
                JoinDate = request.JoinDate,
                IsActive = request.IsActive
            };

            var updatedCustomer = await customerService.UpdateCustomer(customerId, customer);
            var response = new CustomerResponse
            {
                Id = updatedCustomer.Id,
                Name = updatedCustomer.Name,
                Email = updatedCustomer.Email,
                Phone = updatedCustomer.Phone,
                Address = updatedCustomer.Address,
                Discription = updatedCustomer.Discription,
                JoinDate = updatedCustomer.JoinDate,
                IsActive = updatedCustomer.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalTransactions = 0,
                TotalSpent = 0
            };

            return Ok(new ApiResponse<CustomerResponse>
            {
                Success = true,
                Message = "Customer updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<CustomerResponse>
            {
                Success = false,
                Message = "Failed to update customer",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        try
        {
            bool result = await customerService.DeleteCustomer(customerId);
            if (!result)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Customer not found or failed to delete",
                    Errors = new List<string> { "Customer deletion failed" }
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Customer deleted successfully"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete customer",
                Errors = new List<string> { e.Message }
            });
        }
    }
}