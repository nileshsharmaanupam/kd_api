using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("customers")]
public class CustomerDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Discription { get; set; }
    public DateTime? JoinDate { get; set; }
    public bool IsActive { get; set; }
}