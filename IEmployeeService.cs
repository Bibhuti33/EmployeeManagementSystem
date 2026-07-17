using EmployeeManagementSystem.DTOs;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();

        Task<EmployeeDto?> GetByIdAsync(int id);

        Task<int> AddAsync(EmployeeDto dto);

        Task UpdateAsync(EmployeeDto dto);

        Task DeleteAsync(int id);

        Task<IEnumerable<EmployeeDto>> SearchAsync(string keyword);

        Task<IEnumerable<EmployeeDto>> GetPagedAsync(int page, int pageSize);
    }
}
