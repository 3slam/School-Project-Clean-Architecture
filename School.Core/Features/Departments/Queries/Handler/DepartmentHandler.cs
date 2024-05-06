
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;

using School.Core.Features.Departments.Queries.Result;
using School.Core.Wrapper;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Departments.Queries.Handler
{
    public class DepartmentHandler(IStringLocalizer<SharedResourses> localizer, IDepartmentService departmentService, IMapper mapper)
        : ResponseHandler(localizer), IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>
        , IRequestHandler<GetDepartmentByIdQuery, Response<GetSingleDepartmentResponse>>
     , IRequestHandler<GetPaginationDepartmentListQuery, Response<PaginationResponse<GetDepartmentListResponse>>>
    {
        private readonly IDepartmentService departmentService = departmentService;
        private readonly IMapper mapper = mapper;

        public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var list = await departmentService.GetAllDepartment();
            return Success(entity: mapper.Map<List<GetDepartmentListResponse>>(list), message: "Departments retrieved Successfully");
        }

        public async Task<Response<GetSingleDepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Data.Entities.Department department = departmentService.GetById(request.DepartmentId);
            if (department == null)
                return NotFound<GetSingleDepartmentResponse>();
            return Success(mapper.Map<GetSingleDepartmentResponse>(department));
        }

        public async Task<Response<PaginationResponse<GetDepartmentListResponse>>> Handle(GetPaginationDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var querable = departmentService
                .GetQueryableOfDepartmentTableWithFiltertionAndOrdering(request.OrderBy, request.Search);
            var PaginatedList = await mapper.ProjectTo<GetDepartmentListResponse>(querable).ToPaginationListAsync(request.PageNumber, request.PageSize);
            return Success(PaginatedList);

        }
    }
}
