using KD_API.Models;
using KD_API.Models.APIResponse.Cattle;

namespace KD_API.Service.Interfaces;

public interface ICattleService
{
    public Task<CattleResponse> GetCattleById(int cattleId);
    public Task<CattleListResponse> GetAllCattle();
    public Task<bool> CreateCattle(CattleDTO cattleDto);
    public Task<CattleDTO> UpdateCattle(int cattleId, CattleDTO cattleDto);
    public Task<bool> DeleteCattle(int cattleId);
}
