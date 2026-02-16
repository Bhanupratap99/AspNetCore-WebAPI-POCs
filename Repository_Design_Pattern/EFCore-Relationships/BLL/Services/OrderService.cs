using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;
using EFCore_Relationships.Models.Mappers;

namespace EFCore_Relationships.BLL.Services;

/// <summary>
/// Order service implementation containing business logic
/// </summary>
public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;

    public OrderService(IGenericRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    #region Commands

    /// <summary>
    /// Add a new order
    /// </summary>
    public async Task AddOrderAsync(OrderDto dto)
    {
        // Business logic validation
        if (dto.TotalAmount <= 0)
            throw new ArgumentException("Order total amount must be greater than zero");

        var entity = OrderMapper.ToEntity(dto);
        await _orderRepository.AddAsync(entity);
    }

    /// <summary>
    /// Update an existing order
    /// </summary>
    public async Task UpdateOrderAsync(OrderDto dto)
    {
        var existing = await _orderRepository.GetByIdAsync(dto.OrderId);

        if (existing == null)
            throw new KeyNotFoundException($"Order with ID {dto.OrderId} not found");

        OrderMapper.UpdateEntity(existing, dto);
        await _orderRepository.UpdateAsync(existing);
    }

    /// <summary>
    /// Delete an order by ID
    /// </summary>
    public async Task DeleteOrderAsync(int id)
    {
        var existing = await _orderRepository.GetByIdAsync(id);

        if (existing == null)
            throw new KeyNotFoundException($"Order with ID {id} not found");

        await _orderRepository.DeleteAsync(id);
    }

    #endregion

    #region Queries

    /// <summary>
    /// Get all orders
    /// </summary>
    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(OrderMapper.ToDto).ToList();
    }

    /// <summary>
    /// Get an order by ID
    /// </summary>
    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order != null ? OrderMapper.ToDto(order) : null;
    }

    /// <summary>
    /// Get orders by status (Business logic moved to Service layer)
    /// </summary>
    public async Task<List<OrderDto>> GetOrdersByStatusAsync(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty");

        // Filtering logic moved to service layer
        var allOrders = await _orderRepository.GetAllAsync();
        var filteredOrders = allOrders
            .Where(o => o.Status == status)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return filteredOrders.Select(OrderMapper.ToDto).ToList();
    }

    /// <summary>
    /// Get orders by customer email (Business logic moved to Service layer)
    /// </summary>
    public async Task<List<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        // Filtering logic moved to service layer
        var allOrders = await _orderRepository.GetAllAsync();
        var filteredOrders = allOrders
            .Where(o => o.CustomerEmail == email)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return filteredOrders.Select(OrderMapper.ToDto).ToList();
    }

    #endregion
}