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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<DepartmentDto>>
            {
                Success = true,
                Message = "Departments Loaded Successfully",
                Data = departments
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Department Not Found"
                });
            }

            return Ok(new ApiResponse<DepartmentDto>
            {
                Success = true,
                Message = "Department Found",
                Data = department
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            var id = await _service.AddAsync(dto);

            return Ok(new ApiResponse<int>
            {
                Success = true,
                Message = "Department Created Successfully",
                Data = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDto dto)
        {
            await _service.UpdateAsync(dto);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Department Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Department Deleted Successfully"
            });
        }
    }
}
