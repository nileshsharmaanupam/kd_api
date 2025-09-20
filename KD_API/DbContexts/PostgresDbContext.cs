using KD_API.Models;
using Microsoft.EntityFrameworkCore;
namespace KD_API.DbContexts;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        base.Database.EnsureCreated();
        // Removed base.Database.EnsureCreated() from constructor
        // Database creation is now handled in Program.cs during startup
    }
    
    public DbSet<CattleDTO> Cattle { get; set; }
    public DbSet<CustomerDTO> Customers { get; set; }
    public DbSet<ExpenseDTO> Expenses { get; set; }
    public DbSet<PriceDTO> Prices { get; set; }
    public DbSet<ProductDTO> Products { get; set; }
    public DbSet<TransactionDTO> Transactions { get; set; }
    public DbSet<ExpenseTagDTO> ExpenseTags { get; set; }
    public DbSet<CreditNoteDTO> CreditNotes { get; set; }
}