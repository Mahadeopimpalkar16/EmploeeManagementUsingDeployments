using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<Employees> CreateEmployee(Employees employee)
        {
           _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<Employees?> UpdateEmployee(int id, Employees employee)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null)
            {
                return null;
            }
            employee.Id = existing.Id;
            _context.Entry(existing).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) { return false; }

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employees?> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);

        }

    }
}
