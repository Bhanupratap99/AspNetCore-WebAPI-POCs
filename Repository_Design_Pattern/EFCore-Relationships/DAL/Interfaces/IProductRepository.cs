using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.DAL.Interfaces;


// Product repository interface extending generic repository
public interface IProductRepository : IGenericRepository<Product>
{

    Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

    Task<List<Product>> GetProductsInStockAsync();
}