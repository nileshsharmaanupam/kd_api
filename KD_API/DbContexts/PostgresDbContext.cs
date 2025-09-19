using KD_API.Models;
using Microsoft.EntityFrameworkCore;
namespace KD_API.DbContexts;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        base.Database.EnsureCreated();
    }
    
    public DbSet<CattleDTO> Cattle { get; set; }
    public DbSet<CustomerDTO> Customers { get; set; }
    public DbSet<ExpenseDTO> Expenses { get; set; }
    //public DbSet<Price> Prices { get; set; }  Not Going to use for MVP app 
    public DbSet<ProductDTO> Products { get; set; }
    public DbSet<TransactionDTO> Transactions { get; set; }
    public DbSet<ExpenseTagDTO> ExpenseTags { get; set; }
    public DbSet<CreditNoteDTO> CreditNotes { get; set; }
}