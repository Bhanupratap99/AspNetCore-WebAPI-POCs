using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Relationships.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderService.AddOrderAsync(dto);
            return Ok(new { message = "Order created successfully", data = dto });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("UpdateOrder")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderService.UpdateOrderAsync(dto);
            return Ok(new { message = "Order updated successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("DeleteOrder")]
    public async Task<IActionResult> DeleteOrder([FromQuery] int id)
    {
        try
        {
            await _orderService.DeleteOrderAsync(id);
            return Ok(new { message = "Order deleted successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(new { message = "Orders retrieved successfully", count = orders.Count, data = orders });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetOrderById")]
    public async Task<IActionResult> GetOrderById([FromQuery] int id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            return Ok(new { message = "Order retrieved successfully", data = order });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetOrdersByStatus")]
    public async Task<IActionResult> GetOrdersByStatus([FromQuery] string status)
    {
        try
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(new
            {
                message = "Orders retrieved successfully",
                count = orders.Count,
                status,
                data = orders
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetOrdersByCustomerEmail")]
    public async Task<IActionResult> GetOrdersByCustomerEmail([FromQuery] string email)
    {
        try
        {
            var orders = await _orderService.GetOrdersByCustomerEmailAsync(email);
            return Ok(new
            {
                message = "Orders retrieved successfully",
                count = orders.Count,
                customerEmail = email,
                data = orders
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}