using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DAL.Repositories;
using EFCore_Relationships.DATA;
using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore_Relationships.UnitOfWork;

/// <summary>
/// Unit of Work implementation - Coordinates multiple repositories and manages transactions
/// Belongs in DAL but acts as interface between BLL and DAL
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    // Lazy initialization of repositories
    private IGenericRepository<Product>? _productRepository;
    private IGenericRepository<Order>? _orderRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Repository Properties

    /// <summary>
    /// Access to Product repository
    /// </summary>
    public IGenericRepository<Product> Products
    {
        get
        {
            _productRepository ??= new GenericRepository<Product>(_context);
            return _productRepository;
        }
    }

    /// <summary>
    /// Access to Order repository
    /// </summary>
    public IGenericRepository<Order> Orders
    {
        get
        {
            _orderRepository ??= new GenericRepository<Order>(_context);
            return _orderRepository;
        }
    }

    #endregion

    #region Transaction Management

    /// <summary>
    /// Save all changes made in this unit of work
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Begin a new database transaction
    /// </summary>
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commit the current transaction
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No active transaction to commit");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Rollback the current transaction
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Dispose the Unit of Work and release resources
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }

    #endregion
}
