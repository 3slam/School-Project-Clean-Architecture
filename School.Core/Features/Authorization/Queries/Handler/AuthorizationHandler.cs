using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Model;
using School.Core.Features.Authorization.Queries.Response;
using School.Core.Features.Authorization.Queries.Validations;
using School.Core.Wrapper;
using School.Data.Entities;
using School.Data.Resourses;
using School.Service.IService;


namespace School.Core.Features.Authorization.Queries.Handler
{

    public class AuthorizationHandler(
        IStringLocalizer<SharedResourses> localizer,
        IAuthorizationService authorization, IMapper mapper,
        UserManager<User> userManger,
        RoleManager<IdentityRole> roleManager
        )
        : ResponseHandler(localizer),
        IRequestHandler<GetSingleRoleByIdQuery, Response<GetSingleRoleResponse>>,
        IRequestHandler<GetRoleListQuery, Response<PaginationResponse<GetRoleListResponse>>>,
        IRequestHandler<GetUserRolesByUserIdQuery, Response<GetUserRolesByUserIdResponse>>
    {
        public async Task<Response<GetSingleRoleResponse>> Handle(GetSingleRoleByIdQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetSingleRoleByIdValidation(authorization);
            var result = await validator.ValidateAsync(request);
            string error = "";
            if (result.IsValid == false)
            {
                foreach (var item in result.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<GetSingleRoleResponse>(error);
            }

            var roleResult = await authorization.GetRoleById(request.Id);

            if (roleResult == null) BadRequest<GetSingleRoleResponse>("Error in getting role");

            return Success(mapper.Map<GetSingleRoleResponse>(roleResult));
        }

        public async Task<Response<PaginationResponse<GetRoleListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = await authorization.GetRolesListQueryable();
                var paginatedRoleList = await mapper.ProjectTo<GetRoleListResponse>(queryable)
                    .ToPaginationListAsync(request.PageNumber, request.PageSize);
                return Success(paginatedRoleList);
            }
            catch (Exception ex)
            {
                return BadRequest<PaginationResponse<GetRoleListResponse>>(ex.Message);
            }
        }


        public async Task<Response<GetUserRolesByUserIdResponse>> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
        {

            var allRoles = await authorization.GetRolesList();
            GetUserRolesByUserIdResponse userRoles = new GetUserRolesByUserIdResponse();
            var rolesList = new List<UserRole>();

            var user = await userManger.FindByIdAsync(request.Id);

            if (user == null)
                return BadRequest<GetUserRolesByUserIdResponse>("User does not exist.");

            userRoles.UserId = user.Id;
            userRoles.UserName = user.UserName;

            foreach (var role in allRoles)
            {
                bool hasRole = false;
                if (await userManger.IsInRoleAsync(user, role.Name))
                { hasRole = true; }

                rolesList.Add(new UserRole(hasRole, role.Name, role.Id));
            }
           
            userRoles.Roles = rolesList;

            return Success(userRoles);
        }
    }
}