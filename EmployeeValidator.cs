using EmployeeManagementSystem.DTOs;
using FluentValidation;

namespace EmployeeManagementSystem.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0);

            RuleFor(x => x.Salary)
                .GreaterThan(0);
        }
    }
}
