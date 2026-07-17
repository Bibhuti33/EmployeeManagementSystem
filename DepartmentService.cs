using AutoMapper;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();

            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (department == null)
                return null;

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<int> AddAsync(DepartmentDto dto)
        {
            if (await _unitOfWork.Departments.ExistsAsync(x => x.DepartmentName == dto.DepartmentName))
                throw new Exception("Department already exists.");

            var department = _mapper.Map<Department>(dto);

            department.CreatedDate = DateTime.Now;
            department.IsActive = true;

            await _unitOfWork.Departments.AddAsync(department);

            await _unitOfWork.SaveAsync();

            return department.DepartmentId;
        }

        public async Task UpdateAsync(DepartmentDto dto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);

            if (department == null)
                throw new Exception("Department not found.");

            _mapper.Map(dto, department);

            _unitOfWork.Departments.Update(department);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (department == null)
                throw new Exception("Department not found.");

            _unitOfWork.Departments.Delete(department);

            await _unitOfWork.SaveAsync();
        }
    }
}