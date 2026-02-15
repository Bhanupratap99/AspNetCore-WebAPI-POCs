using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.DAL.Interfaces;

// Order repository interface extending generic repository
public interface IOrderRepository : IGenericRepository<Order>
{

    Task<List<Order>> GetOrdersByStatusAsync(string status);
    Task<List<Order>> GetOrdersByCustomerEmailAsync(string email);
}