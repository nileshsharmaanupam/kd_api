namespace KD_API.Models.APIRequests.Cattle;

public class GetCattleByIdRequest
{
    public int CattleId { get; set; }
}

public class GetAllCattleRequest
{
    // Can add filtering/pagination parameters if needed
}

public class CreateCattleRequest
{
    public string Name { get; set; }
    public string Breed { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal? Weight { get; set; }
    public string? Gender { get; set; }
    public string? Color { get; set; }
    public string? HealthStatus { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? LastBreedingDate { get; set; }
    public bool? isLactating { get; set; }
}

public class UpdateCattleRequest
{
    public int CattleId { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal? Weight { get; set; }
    public string? Gender { get; set; }
    public string? Color { get; set; }
    public string? HealthStatus { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? LastBreedingDate { get; set; }
    public bool? IsLactating { get; set; }
    public bool IsActive { get; set; }
}

public class DeleteCattleRequest
{
    public int CattleId { get; set; }
}

// Legacy classes for backward compatibility
public class CreateCattle : CreateCattleRequest { }
public class UpdateCattle : UpdateCattleRequest { }
