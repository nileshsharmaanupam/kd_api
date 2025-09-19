using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Price;
using KD_API.Models.APIResponse.Price;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

[Obsolete("PriceService is new and not included in MVP")]
public class PriceService : IPriceService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public PriceService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PriceResponse> GetPriceById(GetPriceByIdRequest request)
    {
        var price = await _context.Prices.FindAsync(request.PriceId);
        if (price == null)
        {
            throw new ArgumentException($"Price with ID {request.PriceId} not found.");
        }
        
        var response = _mapper.Map<PriceResponse>(price);
        return response;
    }

    public async Task<PriceListResponse> GetAllPrices(GetAllPricesRequest request)
    {
        var prices = await _context.Prices.ToListAsync();
        return _mapper.Map<PriceListResponse>(prices);
    }

    public async Task<PriceResponse> CreatePrice(CreatePriceRequest request)
    {
        var price = _mapper.Map<PriceDTO>(request);
        _context.Prices.Add(price);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<PriceResponse>(price);
        return response;
    }

    public async Task<PriceResponse> UpdatePrice(UpdatePriceRequest request)
    {
        var existingPrice = await _context.Prices.FindAsync(request.PriceId);
        if (existingPrice == null)
        {
            throw new ArgumentException($"Price with ID {request.PriceId} not found.");
        }
        
        _mapper.Map(request, existingPrice);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<PriceResponse>(existingPrice);
        return response;
    }

    public async Task<DeletePriceResponse> DeletePrice(DeletePriceRequest request)
    {
        try
        {
            var price = await _context.Prices.FindAsync(request.PriceId);
            if (price == null)
            {
                return new DeletePriceResponse 
                { 
                    Success = false, 
                    Message = $"Price with ID {request.PriceId} not found." 
                };
            }

            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();
            
            return new DeletePriceResponse 
            { 
                Success = true, 
                Message = "Price deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeletePriceResponse 
            { 
                Success = false, 
                Message = $"Error deleting Price: {ex.Message}" 
            };
        }
    }
}
