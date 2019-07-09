using Honoplay.AdminWebAPI.Interfaces;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Honoplay.AdminWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public (AdminUserAuthenticateModel user, string stringToken) GenerateToken(AdminUserAuthenticateModel user)
        {

            //// authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "AdminUser"),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.UserData, user.TenantId.ToString()),
                    new Claim(ClaimTypes.Webpage, user.HostName)
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            var stringToken = tokenHandler.WriteToken(token);

            var userWithToken = (user, stringToken);
            return userWithToken;
        }
        public string RenewToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtSecurityToken.Claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(jwtToken);

            return stringToken;
        }

    }
}
