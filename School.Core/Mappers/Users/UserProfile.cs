using AutoMapper;
using School.Core.Features.Users.Command.Model;
using School.Core.Features.Users.Queries.Response;
using School.Data.Entities;

namespace School.Core.Mappers.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            MapAddUserCommandUser();
            MapUserToGetUserByEmailResponse();
            MapUserToGetUserPaginationReponse();
        }


        public void MapAddUserCommandUser()
        {

            CreateMap<AddUserCommand, User>()
                  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                  .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                  .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)).ReverseMap();
        }

        public void MapUserToGetUserByEmailResponse()
        {

            CreateMap<User, GetUserByEmailResponse>()
                  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                  .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).ReverseMap();

        }


        public void MapUserToGetUserPaginationReponse()
        {

            CreateMap<User, GetUserPaginationReponse>()
                  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                  .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).ReverseMap();

        }

    }
}
