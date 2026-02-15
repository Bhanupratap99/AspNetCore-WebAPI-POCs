using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DATA;
using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Relationships.DAL.Repositories;

/// <summary>
/// Product repository implementation with product-specific operations
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get products within a specific price range
    /// </summary>
    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _dbSet
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .ToListAsync();
    }

    /// <summary>
    /// Get products that are in stock
    /// </summary>
    public async Task<List<Product>> GetProductsInStockAsync()
    {
        return await _dbSet
            .Where(p => p.Stock > 0)
            .ToListAsync();
    }
}