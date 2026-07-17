using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("employees/pdf")]
        public async Task<IActionResult> EmployeePdf()
        {
            var file = await _service.ExportEmployeesPdfAsync();

            return File(
                file,
                "application/pdf",
                "Employees.pdf");
        }

        [HttpGet("employees/excel")]
        public async Task<IActionResult> EmployeeExcel()
        {
            var file = await _service.ExportEmployeesExcelAsync();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Employees.xlsx");
        }
    }
}
