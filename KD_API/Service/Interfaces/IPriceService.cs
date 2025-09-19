using KD_API.Models;

namespace KD_API.Service.Interfaces;

public interface IPriceService
{
    public Task<PriceDTO> GetPriceById(int priceId);
    public Task<IEnumerable<PriceDTO>> GetAllPrices();
    public Task<bool> CreatePrice(PriceDTO priceDto);
    public Task<PriceDTO> UpdatePrice(int priceId, PriceDTO priceDto);
    public Task<bool> DeletePrice(int priceId);
}
