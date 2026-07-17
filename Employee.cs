namespace EmployeeManagementSystem.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public string Address { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? Photo { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Department? Department { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}
