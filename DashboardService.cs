using EmployeeManagementSystem.Context;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            DashboardDto dashboard = new DashboardDto();

            dashboard.TotalEmployees =
                await _context.Employees.CountAsync();

            dashboard.TotalDepartments =
                await _context.Departments.CountAsync();

            dashboard.PresentToday =
                await _context.Attendances.CountAsync(x =>
                    x.AttendanceDate == DateTime.Today &&
                    x.Status == "Present");

            dashboard.AbsentToday =
                await _context.Attendances.CountAsync(x =>
                    x.AttendanceDate == DateTime.Today &&
                    x.Status == "Absent");

            dashboard.TotalSalary =
                await _context.Employees.SumAsync(x => x.Salary);

            return dashboard;
        }
    }
}