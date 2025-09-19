using System.ComponentModel.DataAnnotations.Schema;

namespace KD_API.Models;

[Table("expenses")]
public class ExpenseDTO
{
    public int Id { get; set; }
    public string Note { get; set; }
    public decimal Amount { get; set; }
    public ExpenseTagDTO? Tag { get; set; }
    public DateTime ExpenseDate { get; set; }
}
