using FluentValidation;
using School.Core.Features.Departments.Commnads.Models;
using School.Service.IService;

namespace School.Core.Features.Departments.Commands.Validations
{
    public class AddDepartmentValidation : AbstractValidator<AddDepartmentCommand>
    {
        public IDepartmentService Service { get; set; }
        public AddDepartmentValidation(IDepartmentService Service)
        {
            this.Service = Service;

            RuleFor(d => d.DeptName)
                .NotEmpty().WithMessage("The {PropertyName} must be not empty")
                .NotNull().WithMessage("The {PropertyName} must be not empty");

            RuleFor(student => student.DeptLocation)
               .NotEmpty().WithMessage("The {PropertyName} must be not empty")
               .NotNull().WithMessage("The {PropertyName} must be not empty");


        }
    }
}
