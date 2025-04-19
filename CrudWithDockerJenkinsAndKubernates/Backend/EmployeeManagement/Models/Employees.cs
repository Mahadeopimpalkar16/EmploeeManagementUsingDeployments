namespace EmployeeManagement.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
    }
}
