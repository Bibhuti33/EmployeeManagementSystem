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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<EmployeeDto>>
            {
                Success = true,
                Message = "Employees Loaded Successfully",
                Data = employees
            });
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ApiResponse<EmployeeDto>
            {
                Success = true,
                Message = "Employee Found",
                Data = employee
            });
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto dto)
        {
            var id = await _service.AddAsync(dto);

            return Ok(new ApiResponse<int>
            {
                Success = true,
                Message = "Employee Created Successfully",
                Data = id
            });
        }

        // PUT: api/Employee
        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDto dto)
        {
            await _service.UpdateAsync(dto);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Employee Updated Successfully"
            });
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Employee Deleted Successfully"
            });
        }

        // GET: api/Employee/search?keyword=abc
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var employees = await _service.SearchAsync(keyword);

            return Ok(new ApiResponse<IEnumerable<EmployeeDto>>
            {
                Success = true,
                Message = "Search Completed",
                Data = employees
            });
        }

        // GET: api/Employee/paged?page=1&pageSize=10
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var employees = await _service.GetPagedAsync(page, pageSize);

            return Ok(new ApiResponse<IEnumerable<EmployeeDto>>
            {
                Success = true,
                Message = "Employees Loaded Successfully",
                Data = employees
            });
        }
    }
}
