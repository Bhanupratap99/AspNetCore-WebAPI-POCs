using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DATA;
using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Relationships.DAL.Repositories;

/// <summary>
/// Order repository implementation with order-specific operations
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get orders by status
    /// </summary>
    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        return await _dbSet
            .Where(o => o.Status == status)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    /// <summary>
    /// Get orders by customer email
    /// </summary>
    public async Task<List<Order>> GetOrdersByCustomerEmailAsync(string email)
    {
        return await _dbSet
            .Where(o => o.CustomerEmail == email)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
}