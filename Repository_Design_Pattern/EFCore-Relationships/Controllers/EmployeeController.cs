using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Relationships.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("CreateEmployee")]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _employeeService.AddEmployeeAsync(dto);
        return Ok(new { message = "Employee created successfully" });
    }

    [HttpPut("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _employeeService.UpdateEmployeeAsync(dto);
            return Ok(new { message = "Employee Updated successfully" });
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return Ok(new { message = "Employee Deleted successfully" });
    }

    [HttpGet("GetAllEmployees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(new { message = "Employee List Retrieved successfully" , data = employees });
    }


    [HttpGet("GetEmployeeById")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);

        if (employee == null)
            return NotFound($"Employee with Id {id} not found");

        return Ok(new { message = $"Employee with Id {id} Retrieved successfully", data = employee });
    }
}