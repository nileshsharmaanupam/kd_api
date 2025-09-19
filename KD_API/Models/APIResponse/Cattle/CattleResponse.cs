namespace KD_API.Models.APIResponse.Cattle;

public class CattleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal? Weight { get; set; }
    public string? Gender { get; set; }
    public string? Color { get; set; }
    public string? HealthStatus { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastBreedingDate { get; set; }
    public bool? isLactating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CattleListResponse
{
    public List<CattleResponse> Cattle { get; set; }
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int InactiveCount { get; set; }
}
