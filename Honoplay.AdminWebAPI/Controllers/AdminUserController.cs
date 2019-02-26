using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI.Services;
using Honoplay.Application.AdminUsers.Commands;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#nullable enable
namespace Honoplay.AdminWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : BaseController
    {
        private readonly AppSettings _appSettings;
        public AdminUserController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateAdminUserCommand command)
        {
            var model = await Mediator.Send(command);
            //// authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "AdminUser"),
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.UserData, model.TenantId.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var stringToken = tokenHandler.WriteToken(token);
            return Ok(new { User = model, Token = stringToken });
        }


    }
}