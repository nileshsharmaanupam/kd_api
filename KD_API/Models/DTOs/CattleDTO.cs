using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("cattle")]
public class CattleDTO
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
}
