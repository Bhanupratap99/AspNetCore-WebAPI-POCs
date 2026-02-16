using EFCore_Relationships.DATA;
using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DAL.Repositories;
using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.BLL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure database and set journal mode to DELETE
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Ensure database is created
    dbContext.Database.EnsureCreated();

    // Disable WAL mode - write directly to main DB file
    dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode = DELETE;");
    dbContext.Database.ExecuteSqlRaw("PRAGMA synchronous = FULL;");

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();