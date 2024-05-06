using School.Data.Entities;

namespace School.Service.IService
{
    public interface IAuthenticationService
    {
        public Task<JwtToken> GenerateJWTToken(User user);
        public Task<JwtToken> RefreshTokenAsync(string token);
    }
}
