using EFCore_Relationships.Models.DTOs;
using EFCore_Relationships.Models.Entities;

namespace EFCore_Relationships.Models.Mappers
{
    /// <summary>
    /// Static mapper class responsible for converting between Employee entity and EmployeeDto.
    /// This follows the Data Transfer Object (DTO) pattern for separating domain models from API contracts.
    /// </summary>
    public static class EmployeeMapper
    {
        /// <summary>
        /// Converts an Employee entity (database model) to EmployeeDto (API response model).
        /// </summary>
        public static EmployeeDto ToDto(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null");

            return new EmployeeDto
            {
                Id = entity.EmployeeID,
                FullName = entity.Name,
                Department = entity.Dept,
                Gender = entity.Gender,
                AnnualSalary = (entity.Salary ?? 0) * 12
            };
        }

        /// <summary>
        /// Converts an EmployeeDto (API request model) to Employee entity (database model).
        /// </summary>
        public static Employee ToEntity(EmployeeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "EmployeeDto cannot be null");

            return new Employee
            {
                Name = dto.FullName,
                Gender = dto.Gender,
                Dept = dto.Department,
                Salary = (int)(dto.AnnualSalary / 12)
            };
        }

        /// <summary>
        /// Updates an existing Employee entity with values from EmployeeDto.
        /// </summary>
        public static void UpdateEntity(Employee entity, EmployeeDto dto)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null");
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "EmployeeDto cannot be null");

            entity.Name = dto.FullName;
            entity.Gender = dto.Gender;
            entity.Dept = dto.Department;
            entity.Salary = (int)(dto.AnnualSalary / 12);
        }
    }
}
