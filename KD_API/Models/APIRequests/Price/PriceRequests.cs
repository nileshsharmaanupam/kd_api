namespace KD_API.Models.APIRequests.Price;

public class GetPriceByIdRequest
{
    public int PriceId { get; set; }
}

public class GetAllPricesRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreatePriceRequest
{
    public int? ProductId { get; set; }
    public int? CattleId { get; set; }
    public decimal PriceValue { get; set; }
    public string PriceType { get; set; } // "Market", "Purchase", "Sale"
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Currency { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
}

public class UpdatePriceRequest
{
    public int PriceId { get; set; }
    public int? ProductId { get; set; }
    public int? CattleId { get; set; }
    public decimal PriceValue { get; set; }
    public string PriceType { get; set; } // "Market", "Purchase", "Sale"
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Currency { get; set; }
    public string Notes { get; set; }
    public bool IsActive { get; set; }
}

public class DeletePriceRequest
{
    public int PriceId { get; set; }
}

// Legacy classes for backward compatibility
public class CreatePrice : CreatePriceRequest { }
public class UpdatePrice : UpdatePriceRequest { }
