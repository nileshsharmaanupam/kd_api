using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface ICattleService
{
    public Task<CattleDTO> GetCattleById(int cattleId);
    public Task<IEnumerable<CattleDTO>> GetAllCattle();
    public Task<bool> CreateCattle(CattleDTO cattleDto);
    public Task<CattleDTO> UpdateCattle(int cattleId, CattleDTO cattleDto);
    public Task<bool> DeleteCattle(int cattleId);
}
