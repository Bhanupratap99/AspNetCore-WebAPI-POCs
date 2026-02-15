using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.Models.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(Product entity)
    {
        return new ProductDto
        {
            ProductId = entity.ProductId,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Stock = entity.Stock
        };
    }

    public static Product ToEntity(ProductDto dto)
    {
        return new Product
        {
            ProductId = dto.ProductId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock
        };
    }

    public static void UpdateEntity(Product entity, ProductDto dto)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.Stock = dto.Stock;
    }
}