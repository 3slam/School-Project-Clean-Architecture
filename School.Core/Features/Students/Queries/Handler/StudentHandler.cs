
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
        IRequestHandler<GetPaginationStudentListQuery, Response<PaginationResponse<GetStudentListResponse>>>,
        IRequestHandler<GetStudentListInDepartmentByDeptIdQuery, Response<List<GetStudentListInDepartmentByDeptIdResponse>>>,
        IRequestHandler<GetStudentWithDepartmentDetailsQuery, Response<List<StudentWithDepartmentDetailsResponse>>>

    {
        private readonly IStudentService studentService = studentService;
        private readonly IMapper mapper = mapper;

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var list = await studentService.GetAllStudents();
            return Success(entity: mapper.Map<List<GetStudentListResponse>>(list), message: "Students retreived Successfully");
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

        public async Task<Response<List<GetStudentListInDepartmentByDeptIdResponse>>> Handle(GetStudentListInDepartmentByDeptIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await studentService.GetDepartmentStudentsProc(request.DepartmentID);
                var resultAfterMapping = mapper.Map<List<GetStudentListInDepartmentByDeptIdResponse>>(result);

                return Success(entity: resultAfterMapping, message: "Students retreived Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<List<GetStudentListInDepartmentByDeptIdResponse>>(ex.Message);
            }
        }

        public async Task<Response<List<StudentWithDepartmentDetailsResponse>>> Handle(GetStudentWithDepartmentDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await studentService.GetStudentWithDepartmentDetailsView();
                var resultAfterMapping = mapper.Map<List<StudentWithDepartmentDetailsResponse>>(result);

                return Success(entity: resultAfterMapping, message: "Students retreived Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<List<StudentWithDepartmentDetailsResponse>>(ex.Message);
            }
        }
    }
}
