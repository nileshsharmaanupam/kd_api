using KD_API.Models.APIRequests.Customer;
using KD_API.Models.APIResponse;
using KD_API.Models.APIResponse.Customer;
using KD_API.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KD_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        try
        {
            var request = new GetCustomerByIdRequest { CustomerId = customerId };
            var response = await _customerService.GetCustomerById(request);
            
            return Ok(new ApiResponse<CustomerResponse>
            {
                Success = true,
                Message = "Customer retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new GetAllCustomersRequest();
            var response = await _customerService.GetAllCustomers(request);
            
            return Ok(new ApiResponse<CustomerListResponse>
            {
                Success = true,
                Message = "Customers retrieved successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CustomerListResponse>
            {
                Success = false,
                Message = "Failed to retrieve customers",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        try
        {
            var response = await _customerService.CreateCustomer(request);
            
            return Ok(new ApiResponse<CustomerResponse>
            {
                Success = true,
                Message = "Customer created successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<CustomerResponse>
            {
                Success = false,
                Message = "Failed to create customer",
                Errors = new List<string> { e.Message }
            });
        }
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerRequest request)
    {
        try
        {
            request.CustomerId = customerId;
            var response = await _customerService.UpdateCustomer(request);

            return Ok(new ApiResponse<CustomerResponse>
            {
                Success = true,
                Message = "Customer updated successfully",
                Data = response
            });
        }
        catch (Exception e)
        {
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
            var request = new DeleteCustomerRequest { CustomerId = customerId };
            var response = await _customerService.DeleteCustomer(request);
            
            if (!response.Success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = response.Message
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = response.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Failed to delete customer",
                Errors = new List<string> { e.Message }
            });
        }
    }
}