using KD_API.DbContexts;
using KD_API.Models.Mappings;
using KD_API.Service.Interfaces;
using KD_API.Service.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

// Configure Entity Framework with PostgreSQL
builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register all service interfaces with their implementations
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICattleService, CattleService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<ICreditNoteService, CreditNoteService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure database is created on application startup
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
//     try
//     {
//         context.Database.EnsureCreated();
//         Console.WriteLine("Database ensured created successfully.");
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Error creating database: {ex.Message}");
//     }
// }

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for Docker access
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
