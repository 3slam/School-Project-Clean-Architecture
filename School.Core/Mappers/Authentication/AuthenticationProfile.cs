using AutoMapper;
using School.Core.Features.Authentication.Command.Result;
using School.Data.Entities;

namespace School.Core.Mappers.Authentication
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<JwtToken, RefreshTokenResult>()
            .ForMember(desination => desination.RefreshToken, opt => opt.MapFrom(source => source.RefreshToken.Token))
            .ForMember(desination => desination.Token, opt => opt.MapFrom(source => source.Token)).
            ForMember(desination => desination.TokenExpirationDate, opt => opt.MapFrom(source => source.ExpirationDate)).
            ForMember(desination => desination.RefreshTokenExpiresOn, opt => opt.MapFrom(source => source.RefreshToken.ExpiresOn));



        }
    }
}
