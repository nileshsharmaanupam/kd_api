using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using KD_API.Models.APIRequests.Cattle;

namespace KD_API.Service.Implementations;

public class CattleService : ICattleService
{
    private readonly PostgresDbContext _context;
    private readonly IMapper _mapper;

    public CattleService(PostgresDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CattleResponse> GetCattleById(int cattleId)
    {
        var cattle = await _context.Cattle.FindAsync(cattleId);
        if (cattle == null)
        {
            throw new ArgumentException($"Cattle with ID {cattleId} not found.");
        }
        
        return _mapper.Map<CattleResponse>(cattle);
    }

    public async Task<CattleListResponse> GetAllCattle()
    {
        var cattleList = await _context.Cattle.ToListAsync();
        return _mapper.Map<CattleListResponse>(cattleList);
    }

    public async Task<bool> CreateCattle(CreateCattle createCattle)
    {
        try
        {
            CattleDTO cattleDto = _mapper.Map<CattleDTO>(createCattle);
            _context.Cattle.Add(cattleDto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CattleResponse> UpdateCattle(int cattleId, UpdateCattle updateCattle)
    {
        CattleDTO? existingCattle = await _context.Cattle.FindAsync(cattleId);
        if (existingCattle == null)
        {
            throw new ArgumentException($"Cattle with ID {cattleId} not found.");
        }
        _mapper.Map(updateCattle, existingCattle);
        await _context.SaveChangesAsync();
        CattleResponse response = _mapper.Map<CattleResponse>(existingCattle);
        return response;
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
