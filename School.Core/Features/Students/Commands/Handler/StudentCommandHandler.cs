using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Commands.Validations;
using School.Data.Constants;
using School.Data.Entities;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Students.Commands.Handler
{
    public class StudentDepartmentHandler(
        IStringLocalizer<SharedResourses> localizer,

        IFileService fileService,
        IStudentService studentService,
        IMapper mapper)
          : ResponseHandler(localizer),
           IRequestHandler<AddStudentCommand, Response<string>>,
           IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentService studentService = studentService;
        private readonly IMapper mapper = mapper;
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddStudentValidation(studentService);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }
            Student afterMapping = mapper.Map<Student>(request);
            afterMapping.Image = await IFormFileToString(request.ImageFile);
            return Created(await studentService.AddStudnetAsync(afterMapping),
                Meta: new { Name = request.FirstName + " " + request.LastName, StudentId = request.Id }
            );
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteStudentValidation(studentService);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var isDeletedSuccess = await studentService.DeleteAsync(mapper.Map<Student>(request));
            if (isDeletedSuccess == "Success") return Deleted<string>();
            return BadRequest<string>(isDeletedSuccess);
        }

        private async Task<string?> IFormFileToString(IFormFile file)
        {
            var imageUrl = await fileService.UploadImage("Instructors", file);

            switch (imageUrl)
            {
                case UplaodingImageState.Error: return null;
                case UplaodingImageState.NoImage: return null;
                default: return imageUrl;
            }

        }
    }


}
