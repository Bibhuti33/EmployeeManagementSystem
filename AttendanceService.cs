using AutoMapper;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendance = await _unitOfWork.Attendances.GetAllAsync();

            return _mapper.Map<IEnumerable<AttendanceDto>>(attendance);
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _unitOfWork.Attendances.GetByIdAsync(id);

            if (attendance == null)
                return null;

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task<int> AddAsync(AttendanceDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var attendance = _mapper.Map<Attendance>(dto);

            await _unitOfWork.Attendances.AddAsync(attendance);

            await _unitOfWork.SaveAsync();

            return attendance.AttendanceId;
        }

        public async Task UpdateAsync(AttendanceDto dto)
        {
            var attendance = await _unitOfWork.Attendances.GetByIdAsync(dto.AttendanceId);

            if (attendance == null)
                throw new Exception("Attendance not found.");

            _mapper.Map(dto, attendance);

            _unitOfWork.Attendances.Update(attendance);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _unitOfWork.Attendances.GetByIdAsync(id);

            if (attendance == null)
                throw new Exception("Attendance not found.");

            _unitOfWork.Attendances.Delete(attendance);

            await _unitOfWork.SaveAsync();
        }
    }
}