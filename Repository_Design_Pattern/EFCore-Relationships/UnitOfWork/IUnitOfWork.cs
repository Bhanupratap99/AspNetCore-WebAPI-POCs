using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.UnitOfWork;

/// <summary>
/// Unit of Work interface - Manages transactions and repository access
/// Acts as a bridge between BLL and DAL
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Generic Repository Access for all entities
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Order> Orders { get; }

    // Transaction Management
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
