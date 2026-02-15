using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Mappers;

namespace EFCore_Relationships.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        // Constructor DI
        private readonly IEmployeeRepository _empRepository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _empRepository = repository;
        }

      
        #region Commands

        public async Task AddEmployeeAsync(EmployeeDto dto)
        {
            var entity = EmployeeMapper.ToEntity(dto);
            await _empRepository.AddAsync(entity);
        }

        public async Task UpdateEmployeeAsync(EmployeeDto dto)
        {
            var existing = await _empRepository.GetByIdAsync(dto.Id);
            if (existing != null)
            {
                EmployeeMapper.UpdateEntity(existing, dto);
                await _empRepository.UpdateAsync(existing);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _empRepository.DeleteAsync(id);
        }

        #endregion

        #region Queries

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _empRepository.GetAllAsync();
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _empRepository.GetByIdAsync(id);
            return employee != null ? EmployeeMapper.ToDto(employee) : null;
        }

        #endregion
    }
}