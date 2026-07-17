using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
