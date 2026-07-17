namespace EmployeeManagementSystem.DTOs
{
    public class DashboardDto
    {
        public int TotalEmployees { get; set; }

        public int TotalDepartments { get; set; }

        public int PresentToday { get; set; }

        public int AbsentToday { get; set; }

        public decimal TotalSalary { get; set; }
    }
}
