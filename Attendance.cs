namespace EmployeeManagementSystem.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public Employee? Employee { get; set; }
    }
}
