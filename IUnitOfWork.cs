using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }

        IGenericRepository<Department> Departments { get; }

        IGenericRepository<Attendance> Attendances { get; }

        IGenericRepository<User> Users { get; }

        Task<int> SaveAsync();
    }
}
