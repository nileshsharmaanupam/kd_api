using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("products")]
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}
