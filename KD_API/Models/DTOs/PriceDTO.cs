using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("prices")]
public class PriceDTO
{
    public int Id { get; set; }
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
