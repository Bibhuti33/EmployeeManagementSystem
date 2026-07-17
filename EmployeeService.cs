using AutoMapper;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<int> AddAsync(EmployeeDto dto)
        {
            if (await _unitOfWork.Employees.ExistsAsync(x => x.Email == dto.Email))
                throw new Exception("Employee Email already exists.");

            var employee = _mapper.Map<Employee>(dto);

            employee.CreatedDate = DateTime.Now;
            employee.IsActive = true;

            await _unitOfWork.Employees.AddAsync(employee);

            await _unitOfWork.SaveAsync();

            return employee.EmployeeId;
        }

        public async Task UpdateAsync(EmployeeDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            _mapper.Map(dto, employee);

            _unitOfWork.Employees.Update(employee);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            _unitOfWork.Employees.Delete(employee);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> SearchAsync(string keyword)
        {
            keyword = keyword.ToLower();

            var employees = await _unitOfWork.Employees.FindAsync(x =>
                x.FirstName.ToLower().Contains(keyword) ||
                x.LastName.ToLower().Contains(keyword) ||
                x.Email.ToLower().Contains(keyword) ||
                x.EmployeeCode.ToLower().Contains(keyword));

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<IEnumerable<EmployeeDto>> GetPagedAsync(int page, int pageSize)
        {
            var employees = await _unitOfWork.Employees.GetPagedAsync(page, pageSize);

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
}