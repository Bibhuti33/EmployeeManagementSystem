using EmployeeManagementSystem.DTOs;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();

        Task<AttendanceDto?> GetByIdAsync(int id);

        Task<int> AddAsync(AttendanceDto dto);

        Task UpdateAsync(AttendanceDto dto);

        Task DeleteAsync(int id);
    }
}
