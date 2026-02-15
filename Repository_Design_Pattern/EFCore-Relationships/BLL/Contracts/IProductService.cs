using EFCore_Relationships.Models.DTOs;

namespace EFCore_Relationships.BLL.Contracts;

/// <summary>
/// Product service interface for business logic operations
/// </summary>
public interface IProductService
{
    // Commands
    Task AddProductAsync(ProductDto dto);
    Task UpdateProductAsync(ProductDto dto);
    Task DeleteProductAsync(int id);

    // Queries
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<List<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<List<ProductDto>> GetProductsInStockAsync();
}