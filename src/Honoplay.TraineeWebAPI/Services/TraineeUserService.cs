﻿using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;
using Honoplay.TraineeUserWebAPI.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Honoplay.TraineeUserWebAPI.Services
{
    public class TraineeUserService : ITraineeUserService
    {
        private readonly AppSettings _appSettings;

        public TraineeUserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public (TraineeUserAuthenticateModel user, string stringToken) GenerateToken(TraineeUserAuthenticateModel user)
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
                    new Claim(ClaimTypes.Role, "TraineeUser"),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Webpage, user.HostName),
                    new Claim(ClaimTypes.GroupSid, user.DepartmentId.ToString()),
                    new Claim(ClaimTypes.UserData, user.TenantId.ToString())
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
