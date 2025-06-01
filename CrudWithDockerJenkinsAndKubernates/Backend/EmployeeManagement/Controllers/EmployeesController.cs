using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;   

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var emp = await _employeeRepository.GetAllAsync();
            return Ok(emp);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _employeeRepository.GetById(id);
            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return BadRequest("Employee Not found");
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employees emp)
        {
            if (emp == null)
            {
                return BadRequest("Invalid employee data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEmp = await _employeeRepository.CreateEmployee(emp);
            return CreatedAtAction(nameof(GetById), new { id = newEmp.Id }, newEmp);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int id, Employees emp)
        {
            var updateEmp = await _employeeRepository.UpdateEmployee(id, emp);
            return updateEmp != null ? Ok(updateEmp) : BadRequest($"{emp.Id} Not found" );
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deletedEmp = await _employeeRepository.DeleteEmployee(id);
            return deletedEmp ? Ok() : NotFound();
        }
    }
}
