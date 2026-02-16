using EFCore_Relationships.DATA;
using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DAL.Repositories;
using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.BLL.Services;
using EFCore_Relationships.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Unit of Work (DAL - Acts as bridge between BLL and DAL)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Generic Repository (optional - only if you need to inject it directly)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register Services (BLL)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Add Controllers
builder.Services.AddControllers();

// Add API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "EF Core Relationships API",
        Version = "v1",
        Description = "API demonstrating Generic Repository Pattern with EF Core"
    });
});

var app = builder.Build();

// Configure database and set journal mode
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Ensure database is created
    dbContext.Database.EnsureCreated();

    // Disable WAL mode - write directly to main DB file
    dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode = DELETE;");
    dbContext.Database.ExecuteSqlRaw("PRAGMA synchronous = FULL;");
}

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EF Core Relationships API v1");
    });
}

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 Application started successfully!");

app.Run();