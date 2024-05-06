using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Service.IService;

namespace School.Core.Features.Students.Commands.Validations
{
    public class DeleteStudentValidation : AbstractValidator<DeleteStudentCommand>
    {
        public IStudentService StudentService { get; set; }
        public DeleteStudentValidation(IStudentService student)
        {
            StudentService = student;


            RuleFor(studentService => studentService.StudentId)
                .MustAsync(async (Id, CancellationToken) =>
                {
                    var result = await student.IsIdExistAsync(Id);
                    return result;
                })
                .WithMessage("The {Id} must be exist");
        }
    }
}
