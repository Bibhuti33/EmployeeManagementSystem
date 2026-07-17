using AutoMapper;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department != null
                        ? src.Department.DepartmentName
                        : ""))
                .ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();

            CreateMap<Attendance, AttendanceDto>().ReverseMap();
        }
    }
}