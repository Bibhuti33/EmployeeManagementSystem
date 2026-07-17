using Microsoft.AspNetCore.Authorization;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attendance = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<AttendanceDto>>
            {
                Success = true,
                Message = "Attendance Loaded Successfully",
                Data = attendance
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendance = await _service.GetByIdAsync(id);

            if (attendance == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Attendance Not Found"
                });
            }

            return Ok(new ApiResponse<AttendanceDto>
            {
                Success = true,
                Message = "Attendance Found",
                Data = attendance
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttendanceDto dto)
        {
            var id = await _service.AddAsync(dto);

            return Ok(new ApiResponse<int>
            {
                Success = true,
                Message = "Attendance Added Successfully",
                Data = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(AttendanceDto dto)
        {
            await _service.UpdateAsync(dto);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Attendance Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Attendance Deleted Successfully"
            });
        }
    }
}
