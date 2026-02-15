using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.Models.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(Order entity)
    {
        return new OrderDto
        {
            OrderId = entity.OrderId,
            CustomerName = entity.CustomerName,
            CustomerEmail = entity.CustomerEmail,
            TotalAmount = entity.TotalAmount,
            OrderDate = entity.OrderDate,
            Status = entity.Status
        };
    }

    public static Order ToEntity(OrderDto dto)
    {
        return new Order
        {
            OrderId = dto.OrderId,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            TotalAmount = dto.TotalAmount,
            OrderDate = dto.OrderDate,
            Status = dto.Status
        };
    }

    public static void UpdateEntity(Order entity, OrderDto dto)
    {
        entity.CustomerName = dto.CustomerName;
        entity.CustomerEmail = dto.CustomerEmail;
        entity.TotalAmount = dto.TotalAmount;
        entity.Status = dto.Status;
    }
}