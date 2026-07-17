using EmployeeManagementSystem.Context;
using EmployeeManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace EmployeeManagementSystem.Services
{
    public class ReportService  : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        //================== Employee PDF ==================

        public async Task<byte[]> ExportEmployeesPdfAsync()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var employees = await _context.Employees
                .Include(x => x.Department)
                .ToListAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header()
                        .Text("Employee Report")
                        .FontSize(22)
                        .Bold();

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Code").Bold();
                                header.Cell().Text("Name").Bold();
                                header.Cell().Text("Department").Bold();
                                header.Cell().Text("Email").Bold();
                                header.Cell().Text("Salary").Bold();
                            });

                            foreach (var emp in employees)
                            {
                                table.Cell().Text(emp.EmployeeCode);
                                table.Cell().Text($"{emp.FirstName} {emp.LastName}");
                                table.Cell().Text(emp.Department?.DepartmentName ?? "");
                                table.Cell().Text(emp.Email);
                                table.Cell().Text(emp.Salary.ToString("0.00"));
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated On : {DateTime.Now:dd-MMM-yyyy}");
                });
            });

            return document.GeneratePdf();
        }

        //================== Employee Excel ==================

        public async Task<byte[]> ExportEmployeesExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var employees = await _context.Employees
                .Include(x => x.Department)
                .ToListAsync();

            using var package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("Employees");

            ws.Cells[1, 1].Value = "Employee Code";
            ws.Cells[1, 2].Value = "First Name";
            ws.Cells[1, 3].Value = "Last Name";
            ws.Cells[1, 4].Value = "Email";
            ws.Cells[1, 5].Value = "Phone";
            ws.Cells[1, 6].Value = "Department";
            ws.Cells[1, 7].Value = "Salary";
            ws.Cells[1, 8].Value = "Joining Date";

            ws.Cells["A1:H1"].Style.Font.Bold = true;

            int row = 2;

            foreach (var emp in employees)
            {
                ws.Cells[row, 1].Value = emp.EmployeeCode;
                ws.Cells[row, 2].Value = emp.FirstName;
                ws.Cells[row, 3].Value = emp.LastName;
                ws.Cells[row, 4].Value = emp.Email;
                ws.Cells[row, 5].Value = emp.Phone;
                ws.Cells[row, 6].Value = emp.Department?.DepartmentName;
                ws.Cells[row, 7].Value = emp.Salary;
                ws.Cells[row, 8].Value = emp.JoiningDate.ToString("dd-MMM-yyyy");

                row++;
            }

            ws.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }

        //================== Attendance PDF ==================

        public Task<byte[]> ExportAttendancePdfAsync()
        {
            throw new NotImplementedException();
        }

        //================== Attendance Excel ==================

        public Task<byte[]> ExportAttendanceExcelAsync()
        {
            throw new NotImplementedException();
        }
    }
}


//        private readonly ApplicationDbContext _context;

//        public ReportService(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<byte[]> ExportEmployeesPdfAsync()
//        {
//            QuestPDF.Settings.License = LicenseType.Community;

//            var employees = await _context.Employees
//                .Include(x => x.Department)
//                .ToListAsync();

//            var document = Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    page.Margin(20);

//                    page.Header()
//                        .Text("Employee Report")
//                        .FontSize(22)
//                        .Bold();

//                    page.Content()
//                        .Table(table =>
//                        {
//                            table.ColumnsDefinition(columns =>
//                            {
//                                columns.RelativeColumn();
//                                columns.RelativeColumn();
//                                columns.RelativeColumn();
//                                columns.RelativeColumn();
//                                columns.RelativeColumn();
//                            });

//                            table.Header(header =>
//                            {
//                                header.Cell().Text("Code").Bold();
//                                header.Cell().Text("Name").Bold();
//                                header.Cell().Text("Department").Bold();
//                                header.Cell().Text("Email").Bold();
//                                header.Cell().Text("Salary").Bold();
//                            });

//                            foreach (var emp in employees)
//                            {
//                                table.Cell().Text(emp.EmployeeCode);

//                                table.Cell().Text(emp.FirstName + " " + emp.LastName);

//                                table.Cell().Text(emp.Department?.DepartmentName ?? "");

//                                table.Cell().Text(emp.Email);

//                                table.Cell().Text(emp.Salary.ToString("0.00"));
//                            }
//                        });

//                    page.Footer()
//                        .AlignCenter()
//                        .Text(x =>
//                        {
//                            x.Span("Generated on ");
//                            x.Span(DateTime.Now.ToString("dd-MMM-yyyy"));
//                        });
//                });
//            });

//            return document.GeneratePdf();
//        }


//        public Task<byte[]> ExportAttendancePdfAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<byte[]> ExportEmployeesExcelAsync()
//         {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//            var employees = await _context.Employees
//                .Include(x => x.Department)
//                .ToListAsync();

//            using var package = new ExcelPackage();

//            var worksheet = package.Workbook.Worksheets.Add("Employees");

//            worksheet.Cells[1, 1].Value = "Employee Code";
//            worksheet.Cells[1, 2].Value = "First Name";
//            worksheet.Cells[1, 3].Value = "Last Name";
//            worksheet.Cells[1, 4].Value = "Email";
//            worksheet.Cells[1, 5].Value = "Phone";
//            worksheet.Cells[1, 6].Value = "Department";
//            worksheet.Cells[1, 7].Value = "Salary";
//            worksheet.Cells[1, 8].Value = "Joining Date";

//            using (var range = worksheet.Cells[1, 1, 1, 8])
//            {
//                range.Style.Font.Bold = true;
//            }

//            int row = 2;

//            foreach (var emp in employees)
//            {
//                worksheet.Cells[row, 1].Value = emp.EmployeeCode;
//                worksheet.Cells[row, 2].Value = emp.FirstName;
//                worksheet.Cells[row, 3].Value = emp.LastName;
//                worksheet.Cells[row, 4].Value = emp.Email;
//                worksheet.Cells[row, 5].Value = emp.Phone;
//                worksheet.Cells[row, 6].Value = emp.Department?.DepartmentName;
//                worksheet.Cells[row, 7].Value = emp.Salary;
//                worksheet.Cells[row, 8].Value = emp.JoiningDate.ToString("dd-MMM-yyyy");

//                row++;
//            }

//            worksheet.Cells.AutoFitColumns();

//            return package.GetAsByteArray();


//        }
//    }
//}