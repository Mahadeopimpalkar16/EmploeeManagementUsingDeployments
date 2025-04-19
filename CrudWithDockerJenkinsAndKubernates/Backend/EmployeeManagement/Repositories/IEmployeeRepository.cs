using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAllAsync();
        Task<Employees?> GetById(int id);
        Task<Employees> CreateEmployee(Employees employee);
        Task<Employees?> UpdateEmployee(int id, Employees employee);
        Task <bool>DeleteEmployee(int id);    
    }
}
