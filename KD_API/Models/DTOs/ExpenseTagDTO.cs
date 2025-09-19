using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("expense_tags")]
public class ExpenseTagDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}