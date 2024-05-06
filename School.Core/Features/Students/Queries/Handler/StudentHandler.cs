
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Result;
using School.Core.Wrapper;
using School.Data.Entities;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Students.Queries.Handler
{
    public class DepartmentHandler(
        IStringLocalizer<SharedResourses> localizer,
        IStudentService studentService, IMapper mapper)
        : ResponseHandler(localizer),
        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
        IRequestHandler<GetPaginationStudentListQuery, Response<PaginationResponse<GetStudentListResponse>>>
    {
        private readonly IStudentService studentService = studentService;
        private readonly IMapper mapper = mapper;

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var list = await studentService.GetAllStudents();
            return Success(entity: mapper.Map<List<GetStudentListResponse>>(list), message: "Students retreved Successfully");
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            Student student = studentService.GetById(request.StudentId);
            if (student == null)
                return NotFound<GetSingleStudentResponse>();
            return Success(mapper.Map<GetSingleStudentResponse>(student));
        }

        public async Task<Response<PaginationResponse<GetStudentListResponse>>> Handle(GetPaginationStudentListQuery request, CancellationToken cancellationToken)
        {
            var querable = studentService.
           GetQueryableOfStudentTableWithFiltertionAndOrdering(request.OrderBy, request.Search);

            var PaginatedList = await mapper.ProjectTo<GetStudentListResponse>(querable)
                .ToPaginationListAsync(request.PageNumber, request.PageSize);
            return Success(PaginatedList);

        }
    }
}
