using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.DAL.Interfaces;

public interface IEmployeeRepository
{
    #region Commands (do something [change state])

    // Create
    Task AddAsync(Employee employee);

    // Update
    Task UpdateAsync(Employee employee);

    // Delete
    Task DeleteAsync(int id);

    #endregion

    #region Queries (ask for something [return data])

    // Get all employees
    Task<List<Employee>> GetAllAsync();

    // Get employee by ID
    Task<Employee?> GetByIdAsync(int id);

    #endregion
}
