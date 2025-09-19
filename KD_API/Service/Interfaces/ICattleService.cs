using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIResponse.Cattle;

namespace KD_API.Service.Interfaces;

public interface ICattleService
{
    public Task<CattleResponse> GetCattleById(GetCattleByIdRequest request);
    public Task<CattleListResponse> GetAllCattle(GetAllCattleRequest request);
    public Task<CattleResponse> CreateCattle(CreateCattleRequest request);
    public Task<CattleResponse> UpdateCattle(UpdateCattleRequest request);
    public Task<DeleteCattleResponse> DeleteCattle(DeleteCattleRequest request);
}
