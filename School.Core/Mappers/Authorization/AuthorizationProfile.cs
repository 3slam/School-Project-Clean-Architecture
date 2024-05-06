using AutoMapper;
using Microsoft.AspNetCore.Identity;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Mappers.Authorization
{
    public class AuthorizationProfile : Profile

    {
        public AuthorizationProfile()
        {
            RoleIdentityToGetSingleRoleResponse();
            RoleIdentityToGetRoleListResponse();
        }

        public void RoleIdentityToGetSingleRoleResponse()
        {
            CreateMap<IdentityRole, GetSingleRoleResponse>().
             ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
        }
        public void RoleIdentityToGetRoleListResponse()
        {
            CreateMap<IdentityRole, GetRoleListResponse>().
             ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
