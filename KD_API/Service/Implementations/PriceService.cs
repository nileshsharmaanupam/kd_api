using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class PriceService : IPriceService
{
    private readonly PostgresDbContext _context;

    public PriceService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<PriceDTO> GetPriceById(int priceId)
    {
        var price = await _context.Set<PriceDTO>().FindAsync(priceId);
        if (price == null)
        {
            throw new ArgumentException($"Price with ID {priceId} not found.");
        }
        return price;
    }

    public async Task<IEnumerable<PriceDTO>> GetAllPrices()
    {
        return await _context.Set<PriceDTO>().ToListAsync();
    }

    public async Task<bool> CreatePrice(PriceDTO priceDto)
    {
        try
        {
            _context.Set<PriceDTO>().Add(priceDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<PriceDTO> UpdatePrice(int priceId, PriceDTO priceDto)
    {
        var existingPrice = await _context.Set<PriceDTO>().FindAsync(priceId);
        if (existingPrice == null)
        {
            throw new ArgumentException($"Price with ID {priceId} not found.");
        }

        existingPrice.ProductId = priceDto.ProductId;
        existingPrice.CattleId = priceDto.CattleId;
        existingPrice.PriceValue = priceDto.PriceValue;
        existingPrice.PriceType = priceDto.PriceType;
        existingPrice.EffectiveDate = priceDto.EffectiveDate;
        existingPrice.ExpiryDate = priceDto.ExpiryDate;
        existingPrice.Currency = priceDto.Currency;
        existingPrice.Notes = priceDto.Notes;
        existingPrice.IsActive = priceDto.IsActive;

        await _context.SaveChangesAsync();
        return existingPrice;
    }

    public async Task<bool> DeletePrice(int priceId)
    {
        try
        {
            var price = await _context.Set<PriceDTO>().FindAsync(priceId);
            if (price == null)
            {
                return false;
            }

            _context.Set<PriceDTO>().Remove(price);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
