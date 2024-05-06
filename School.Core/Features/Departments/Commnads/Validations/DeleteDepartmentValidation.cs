using FluentValidation;
using School.Core.Features.Departments.Commnads.Models;
using School.Service.IService;

namespace School.Core.Features.Departments.Commands.Validations
{
    public class DeleteDepartmentValidation : AbstractValidator<DeleteDepartmentCommand>
    {
        public IDepartmentService Service { get; set; }
        public DeleteDepartmentValidation(IDepartmentService Service)
        {
            this.Service = Service;


            RuleFor(d => d.DepartmentId)
                .MustAsync(async (Id, CancellationToken) =>
                {
                    var result = await Service.IsIdExistAsync(Id);
                    return result;
                })
                .WithMessage("The {Id} must be exist");
        }
    }
}
