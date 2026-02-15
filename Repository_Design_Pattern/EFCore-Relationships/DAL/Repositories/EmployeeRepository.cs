using EFCore_Relationships.BLL.Contracts;
using EFCore_Relationships.DAL.Interfaces;
using EFCore_Relationships.DATA;
using EFCore_Relationships.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Relationships.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor DI
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Commands

        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region Queries

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                                 .FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        #endregion
    }
}
