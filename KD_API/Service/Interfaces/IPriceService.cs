using KD_API.Models.APIRequests.Price;
using KD_API.Models.APIResponse.Price;

namespace KD_API.Service.Interfaces;

public interface IPriceService
{
    public Task<PriceResponse> GetPriceById(GetPriceByIdRequest request);
    public Task<PriceListResponse> GetAllPrices(GetAllPricesRequest request);
    public Task<PriceResponse> CreatePrice(CreatePriceRequest request);
    public Task<PriceResponse> UpdatePrice(UpdatePriceRequest request);
    public Task<DeletePriceResponse> DeletePrice(DeletePriceRequest request);
}
