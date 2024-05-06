using FluentValidation;
using School.Core.Features.Departments.Commnads.Models;
using School.Service.IService;

namespace School.Core.Features.Department.Commands.Validations
{
    public class UpdateStudentValidation : AbstractValidator<UpdateDepartmentCommand>
    {
        public IDepartmentService Service { get; set; }
        public UpdateStudentValidation(IDepartmentService service)
        {
            this.Service = Service;

            RuleFor(d => d.DeptName)
                .NotEmpty().WithMessage("The {PropertyName} must be not empty")
                .NotNull().WithMessage("The {PropertyName} must be not empty");

            RuleFor(student => student.DeptLocation)
               .NotEmpty().WithMessage("The {PropertyName} must be not empty")
               .NotNull().WithMessage("The {PropertyName} must be not empty");

            RuleFor(d => d.DepartmentId)
                .MustAsync(async (Id, CancellationToken) =>
                {
                    var result = await Service.IsIdExistOnceAsync(Id);
                    return !result;
                })
                .WithMessage("The {PropertyName} must be exist in database to updata it!");
        }
    }
}
