namespace KD_API.Models.APIResponse.Price;

public class PriceResponse
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public int? CattleId { get; set; }
    public string? CattleName { get; set; }
    public decimal PriceValue { get; set; }
    public string PriceType { get; set; } // "Market", "Purchase", "Sale"
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Currency { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsExpired { get; set; }
    public int DaysUntilExpiry { get; set; }
}

public class PriceListResponse
{
    public List<PriceResponse> Prices { get; set; }
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int ExpiredCount { get; set; }
    public Dictionary<string, decimal> AveragePriceByType { get; set; }
    public Dictionary<string, int> PriceCountByType { get; set; }
}
