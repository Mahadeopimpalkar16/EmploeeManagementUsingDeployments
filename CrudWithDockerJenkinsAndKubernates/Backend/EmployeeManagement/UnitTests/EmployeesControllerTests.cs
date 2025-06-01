using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeManagement.UnitTests
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeesController _EmpController;

        public EmployeesControllerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _EmpController = new EmployeesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<Employees> { new Employees { Id = 1, FullName = "Test" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _EmpController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employees, okResult.Value);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOkResult()
        {
            var employee = new Employees { Id = 1, FullName = "Test" };
            _mockRepo.Setup(repo => repo.GetById(1)).ReturnsAsync(employee);

            var result = await _EmpController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employee, okResult.Value);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsBadRequest()
        {
            _mockRepo.Setup(repo => repo.GetById(2)).ReturnsAsync((Employees)null);

            var result = await _EmpController.GetById(2);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtAction()
        {
            var employee = new Employees { Id = 10,
                                            FullName = "Jane Doe",
                                            Email = "JaneDoe@abc.com",
                                            Department = "HR",
                                            Gender = "Male",
                                            DateOfJoining = DateTime.Now
                                         };
            _mockRepo.Setup(repo => repo.CreateEmployee(employee)).ReturnsAsync(employee);

            var result = await _EmpController.CreateEmployee(employee);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(employee, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateEmployee_ExistingId_ReturnsOkResult()
        {
            var employee = new Employees { Id = 1, FullName = "Test" };
            _mockRepo.Setup(repo => repo.UpdateEmployee(1, employee)).ReturnsAsync(employee);

            var result = await _EmpController.UpdateEmployee(1, employee);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employee, okResult.Value);
        }

        [Fact]
        public async Task UpdateEmployee_NonExistingId_ReturnsBadRequest()
        {
            var employee = new Employees { Id = 2, FullName = "Test" };
            _mockRepo.Setup(repo => repo.UpdateEmployee(2, employee)).ReturnsAsync((Employees)null);

            var result = await _EmpController.UpdateEmployee(2, employee);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingId_ReturnsOk()
        {
            _mockRepo.Setup(repo => repo.DeleteEmployee(1)).ReturnsAsync(true);

            var result = await _EmpController.DeleteEmployee(1);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_NonExistingId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.DeleteEmployee(2)).ReturnsAsync(false);

            var result = await _EmpController.DeleteEmployee(2);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}