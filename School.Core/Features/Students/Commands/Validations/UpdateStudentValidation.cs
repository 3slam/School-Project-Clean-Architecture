using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Service.IService;

namespace School.Core.Features.Students.Commands.Validations
{
    public class UpdateStudentValidation : AbstractValidator<AddStudentCommand>
    {
        public IDepartmentService StudentService { get; set; }
        public UpdateStudentValidation(IDepartmentService studentService)
        {
            StudentService = studentService;

            RuleFor(student => student.FirstName)
                .NotEmpty().WithMessage("The {PropertName} must be not empty")
                .NotNull().WithMessage("The {PropertName} must be not empty");

            RuleFor(student => student.LastName)
               .NotEmpty().WithMessage("The {PropertName} must be not empty")
               .NotNull().WithMessage("The {PropertName} must be not empty");

            RuleFor(studentService => studentService.Id)
                .MustAsync(async (Id, CancellationToken) =>
                {
                    var result = await StudentService.IsIdExistAsync(Id);
                    return !result;
                })
                .WithMessage("The {Id} must be not unique");
        }
    }
}
