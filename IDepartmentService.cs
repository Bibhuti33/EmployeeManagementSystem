using EmployeeManagementSystem.DTOs;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();

        Task<DepartmentDto?> GetByIdAsync(int id);

        Task<int> AddAsync(DepartmentDto dto);

        Task UpdateAsync(DepartmentDto dto);

        Task DeleteAsync(int id);
    }
}
