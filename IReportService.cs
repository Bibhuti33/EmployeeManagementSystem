using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> ExportEmployeesPdfAsync();

        Task<byte[]> ExportEmployeesExcelAsync();

        Task<byte[]> ExportAttendancePdfAsync();

        Task<byte[]> ExportAttendanceExcelAsync();
    }
}
