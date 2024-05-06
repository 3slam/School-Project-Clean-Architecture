using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Commands.Validations;
using School.Core.Features.Departments.Commnads.Models;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Departments.Commands.Handler
{
    public class DepartmentCommandHandler(IStringLocalizer<SharedResourses> localizer, IDepartmentService departmentService, IMapper mapper)
        : ResponseHandler(localizer),
           IRequestHandler<AddDepartmentCommand, Response<string>>,
           IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        private readonly IDepartmentService departmentService = departmentService;
        private readonly IMapper mapper = mapper;
        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddDepartmentValidation(departmentService);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            return Created(await departmentService.AddDepartmentAsync(mapper.Map<Data.Entities.Department>(request)),
                Meta: new { Name = request.DeptName, Location = request.DeptLocation }
                );
        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteDepartmentValidation(departmentService);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var isDeletedSuccess = await departmentService.DeleteAsync(mapper.Map<Data.Entities.Department>(request));
            if (isDeletedSuccess == "Success") return Deleted<string>();
            return BadRequest<string>(isDeletedSuccess);
        }
    }
}
