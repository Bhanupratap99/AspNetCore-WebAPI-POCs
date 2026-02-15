using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.BLL.Contracts
{
    public interface IEmployeeService
    {

        // Commands
        Task AddEmployeeAsync(EmployeeDto dto);
        Task UpdateEmployeeAsync(EmployeeDto dto);
        Task DeleteEmployeeAsync(int id);


        // Queries
        Task<List<EmployeeDto?>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    }
}

