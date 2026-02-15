using EFCore_Relationships.Models.DTOs;

namespace EFCore_Relationships.BLL.Contracts;

/// <summary>
/// Order service interface for business logic operations
/// </summary>
public interface IOrderService
{
    // Commands
    Task AddOrderAsync(OrderDto dto);
    Task UpdateOrderAsync(OrderDto dto);
    Task DeleteOrderAsync(int id);

    // Queries
    Task<List<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<List<OrderDto>> GetOrdersByStatusAsync(string status);
    Task<List<OrderDto>> GetOrdersByCustomerEmailAsync(string email);
}