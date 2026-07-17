using EmployeeManagementSystem.Context;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new GenericRepository<Employee>(_context);

            Departments = new GenericRepository<Department>(_context);

            Attendances = new GenericRepository<Attendance>(_context);

            Users = new GenericRepository<User>(_context);
        }

        public IGenericRepository<Employee> Employees { get; }

        public IGenericRepository<Department> Departments { get; }

        public IGenericRepository<Attendance> Attendances { get; }

        public IGenericRepository<User> Users { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}