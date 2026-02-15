using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Relationships.DATA;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

}
