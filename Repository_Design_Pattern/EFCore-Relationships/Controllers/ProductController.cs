using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Relationships.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.AddProductAsync(dto);
            return Ok(new { message = "Product created successfully", data = dto });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.UpdateProductAsync(dto);
            return Ok(new { message = "Product updated successfully" });
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

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct([FromQuery] int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new { message = "Product deleted successfully" });
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

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new { message = "Products retrieved successfully", count = products.Count, data = products });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetProductById")]
    public async Task<IActionResult> GetProductById([FromQuery] int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound(new { message = $"Product with ID {id} not found" });

            return Ok(new { message = "Product retrieved successfully", data = product });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetProductsByPriceRange")]
    public async Task<IActionResult> GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        try
        {
            var products = await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(new
            {
                message = "Products retrieved successfully",
                count = products.Count,
                priceRange = new { minPrice, maxPrice },
                data = products
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

    [HttpGet("GetProductsInStock")]
    public async Task<IActionResult> GetProductsInStock()
    {
        try
        {
            var products = await _productService.GetProductsInStockAsync();
            return Ok(new { message = "Products in stock retrieved successfully", count = products.Count, data = products });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}