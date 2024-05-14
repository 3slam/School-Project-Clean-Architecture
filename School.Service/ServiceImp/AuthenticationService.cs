using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities;
using School.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Service.ServiceImp
{
    public class AuthenticationService(IConfiguration configuration, UserManager<User> userManager)
        : IAuthenticationService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly UserManager<User> userManager = userManager;
        public async Task<JwtToken> GenerateJWTToken(User user)
        {

            var token = await GenerateJwtSecurityToken(user);
            var refreshToken = GenerateRefreshToken();

            var jwtToken = new JwtToken();

            jwtToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            jwtToken.ExpirationDate = token.ValidTo;
            jwtToken.RefreshToken = refreshToken;

            return jwtToken;
        }

        public async Task<JwtToken> RefreshTokenAsync(string token)
        {

            var jwtToken = new JwtToken();

            var user = userManager.Users.SingleOrDefault(u => u.RefreshToken.Token == token);

            if (user == null)
            {
                throw new Exception("Invalid token");
            }

            var refreshToken = user.RefreshToken.Token;

            if (!user.RefreshToken.IsActive)
            {
                throw new Exception("Inactive token");
            }



            var newRefreshToken = GenerateRefreshToken();
            var newtoken = await GenerateJwtSecurityToken(user);

            string tokenAsAstring = new JwtSecurityTokenHandler().WriteToken(newtoken);

            jwtToken.Token = tokenAsAstring;
            jwtToken.ExpirationDate = DateTime.Now.AddMinutes(60).ToLocalTime();
            jwtToken.RefreshToken = newRefreshToken;

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);

            return jwtToken;
        }

        private async Task<JwtSecurityToken> GenerateJwtSecurityToken(User user)
        {

            var claims = new List<Claim>()
            {
              new Claim(ClaimTypes.NameIdentifier,user.Id),
              new Claim(ClaimTypes.Name,user.FullName),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.NameIdentifier, user.Id),
              new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
            };

            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["jwtSettings:secretkey"]));

            var token = new JwtSecurityToken
                (issuer: configuration["jwtSettings:validateIssuer"],
                 audience: configuration["jwtSettings:validateAudience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(60).ToLocalTime(),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10).ToLocalTime(),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
