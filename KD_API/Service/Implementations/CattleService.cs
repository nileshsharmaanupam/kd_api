using AutoMapper;
using KD_API.DbContexts;
using KD_API.Models;
using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<CattleResponse> GetCattleById(GetCattleByIdRequest request)
    {
        var cattle = await _context.Cattle.FindAsync(request.CattleId);
        if (cattle == null)
        {
            throw new ArgumentException($"Cattle with ID {request.CattleId} not found.");
        }
        
        var response = _mapper.Map<CattleResponse>(cattle);
        return response;
    }

    public async Task<CattleListResponse> GetAllCattle(GetAllCattleRequest request)
    {
        var cattle = await _context.Cattle.ToListAsync();
        return _mapper.Map<CattleListResponse>(cattle);
    }

    public async Task<CattleResponse> CreateCattle(CreateCattleRequest request)
    {
        var cattle = _mapper.Map<CattleDTO>(request);
        _context.Cattle.Add(cattle);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CattleResponse>(cattle);
        return response;
    }

    public async Task<CattleResponse> UpdateCattle(UpdateCattleRequest request)
    {
        var existingCattle = await _context.Cattle.FindAsync(request.CattleId);
        if (existingCattle == null)
        {
            throw new ArgumentException($"Cattle with ID {request.CattleId} not found.");
        }
        
        _mapper.Map(request, existingCattle);
        await _context.SaveChangesAsync();
        
        var response = _mapper.Map<CattleResponse>(existingCattle);
        return response;
    }

    public async Task<DeleteCattleResponse> DeleteCattle(DeleteCattleRequest request)
    {
        try
        {
            var cattle = await _context.Cattle.FindAsync(request.CattleId);
            if (cattle == null)
            {
                return new DeleteCattleResponse 
                { 
                    Success = false, 
                    Message = $"Cattle with ID {request.CattleId} not found." 
                };
            }

            _context.Cattle.Remove(cattle);
            await _context.SaveChangesAsync();
            
            return new DeleteCattleResponse 
            { 
                Success = true, 
                Message = "Cattle deleted successfully." 
            };
        }
        catch (Exception ex)
        {
            return new DeleteCattleResponse 
            { 
                Success = false, 
                Message = $"Error deleting Cattle: {ex.Message}" 
            };
        }
    }
}
