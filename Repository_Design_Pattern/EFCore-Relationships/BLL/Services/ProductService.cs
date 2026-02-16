using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;
using EFCore_Relationships.Models.Mappers;
using EFCore_Relationships.UnitOfWork;

namespace EFCore_Relationships.BLL.Services;

/// <summary>
/// Product service implementation containing business logic
/// Uses Unit of Work for data access
/// </summary>
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Commands

    /// <summary>
    /// Add a new product
    /// </summary>
    public async Task AddProductAsync(ProductDto dto)
    {
        var entity = ProductMapper.ToEntity(dto);
        await _unitOfWork.Products.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    public async Task UpdateProductAsync(ProductDto dto)
    {
        var existing = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);

        if (existing == null)
            throw new KeyNotFoundException($"Product with ID {dto.ProductId} not found");

        ProductMapper.UpdateEntity(existing, dto);
        await _unitOfWork.Products.UpdateAsync(existing);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a product by ID
    /// </summary>
    public async Task DeleteProductAsync(int id)
    {
        var existing = await _unitOfWork.Products.GetByIdAsync(id);

        if (existing == null)
            throw new KeyNotFoundException($"Product with ID {id} not found");

        await _unitOfWork.Products.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    #endregion

    #region Queries

    /// <summary>
    /// Get all products
    /// </summary>
    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return products.Select(ProductMapper.ToDto).ToList();
    }

    /// <summary>
    /// Get a product by ID
    /// </summary>
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        return product != null ? ProductMapper.ToDto(product) : null;
    }

    /// <summary>
    /// Get products within a specific price range (Business logic moved to Service layer)
    /// </summary>
    public async Task<List<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0 || maxPrice < 0)
            throw new ArgumentException("Price values cannot be negative");

        if (minPrice > maxPrice)
            throw new ArgumentException("Minimum price cannot be greater than maximum price");

        // Filtering logic moved to service layer
        var allProducts = await _unitOfWork.Products.GetAllAsync();
        var filteredProducts = allProducts
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .ToList();

        return filteredProducts.Select(ProductMapper.ToDto).ToList();
    }

    /// <summary>
    /// Get all products that are in stock (Business logic moved to Service layer)
    /// </summary>
    public async Task<List<ProductDto>> GetProductsInStockAsync()
    {
        // Filtering logic moved to service layer
        var allProducts = await _unitOfWork.Products.GetAllAsync();
        var inStockProducts = allProducts
            .Where(p => p.Stock > 0)
            .ToList();

        return inStockProducts.Select(ProductMapper.ToDto).ToList();
    }

    #endregion
}