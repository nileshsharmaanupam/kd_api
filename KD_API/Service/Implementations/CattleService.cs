using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KD_API.Service.Implementations;

public class CattleService : ICattleService
{
    private readonly PostgresDbContext _context;

    public CattleService(PostgresDbContext context)
    {
        _context = context;
    }

    public async Task<CattleDTO> GetCattleById(int cattleId)
    {
        var cattle = await _context.Cattle.FindAsync(cattleId);
        if (cattle == null)
        {
            throw new ArgumentException($"Cattle with ID {cattleId} not found.");
        }
        return cattle;
    }

    public async Task<IEnumerable<CattleDTO>> GetAllCattle()
    {
        return await _context.Cattle.ToListAsync();
    }

    public async Task<bool> CreateCattle(CattleDTO cattleDto)
    {
        try
        {
            _context.Cattle.Add(cattleDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CattleDTO> UpdateCattle(int cattleId, CattleDTO cattleDto)
    {
        var existingCattle = await _context.Cattle.FindAsync(cattleId);
        if (existingCattle == null)
        {
            throw new ArgumentException($"Cattle with ID {cattleId} not found.");
        }

        existingCattle.Name = cattleDto.Name;
        existingCattle.Breed = cattleDto.Breed;
        existingCattle.BirthDate = cattleDto.BirthDate;
        existingCattle.Weight = cattleDto.Weight;
        existingCattle.Gender = cattleDto.Gender;
        existingCattle.Color = cattleDto.Color;
        existingCattle.HealthStatus = cattleDto.HealthStatus;
        existingCattle.PurchaseDate = cattleDto.PurchaseDate;
        existingCattle.PurchasePrice = cattleDto.PurchasePrice;
        existingCattle.IsActive = cattleDto.IsActive;
        existingCattle.LastBreedingDate = cattleDto.LastBreedingDate;
        existingCattle.isLactating = cattleDto.isLactating;

        await _context.SaveChangesAsync();
        return existingCattle;
    }

    public async Task<bool> DeleteCattle(int cattleId)
    {
        try
        {
            var cattle = await _context.Cattle.FindAsync(cattleId);
            if (cattle == null)
            {
                return false;
            }

            _context.Cattle.Remove(cattle);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
