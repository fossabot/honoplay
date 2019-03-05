using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

#nullable enable

namespace Honoplay.AdminWebAPI.Controllers
{
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
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, "AdminUser"),
                    new Claim(ClaimTypes.Name, model.Name),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var stringToken = tokenHandler.WriteToken(token);
            return Ok(new { User = model, Token = stringToken });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAdminUserCommand command)
        {
            try
            {
                var model = await Mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<AdminUserRegisterModel>(new Error(409, HttpStatusCode.Conflict.ToString(), ex)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AdminUserRegisterModel>(new Error(500, HttpStatusCode.InternalServerError.ToString(), ex)));
            }
        }
    }
}