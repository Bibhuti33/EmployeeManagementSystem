using EmployeeManagementSystem.DTOs;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);

        Task RegisterAsync(RegisterDto dto);
    }
}
