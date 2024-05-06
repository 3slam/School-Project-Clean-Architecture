using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Users.Queries.Models;
using School.Core.Features.Users.Queries.Response;
using School.Core.Wrapper;
using School.Data.Entities;
using School.Data.Resourses;

namespace School.Core.Features.Users.Queries.Handler
{
    public class UserQueryHandler(
        IStringLocalizer<SharedResourses> localizer, IMapper mapper, UserManager<User> userManager)
        : ResponseHandler(localizer),
        IRequestHandler<GetUserByEmailQuery, Response<GetUserByEmailResponse>>,
       IRequestHandler<GetAllUsersListPaginationQuery, Response<PaginationResponse<GetUserPaginationReponse>>>
    {
        private readonly IMapper mapper = mapper;
        private readonly UserManager<User> userManager = userManager;
        public async Task<Response<GetUserByEmailResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
                return NotFound<GetUserByEmailResponse>("User does not exist in the system");
            return Success(mapper.Map<GetUserByEmailResponse>(user));
        }

        public async Task<Response<PaginationResponse<GetUserPaginationReponse>>> Handle(GetAllUsersListPaginationQuery request, CancellationToken cancellationToken)
        {
            var querable = userManager.Users.AsQueryable();
            if (request.searach != null)
                querable = querable.Where(u => u.FullName.Contains(request.searach));
            var PaginatedList = await
                mapper.ProjectTo<GetUserPaginationReponse>(querable)
               .ToPaginationListAsync(request.PageNumber, request.PageSize);

            return Success(PaginatedList);
        }
    }
}
