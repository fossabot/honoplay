using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Honoplay.Application.Exceptions;
using Honoplay.Common.Extensions;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{

    public class AuthenticateAdminUserCommandHandler : IRequestHandler<AuthenticateAdminUserCommand, AdminUserAuthenticateModel>
    {
        private readonly HonoplayDbContext _context;


        public AuthenticateAdminUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }


        public async Task<AdminUserAuthenticateModel> Handle(AuthenticateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _context.AdminUsers
                                    .Where(u => u.Username == request.Username
                                             && u.TenantId == request.TenantId)
                                    .ToListAsync();

            if (adminUser.Count < 1 || adminUser.Count > 1)
            {
                throw new NotFoundException(nameof(request.Username), request.Username);
            }

            var salt = adminUser[0].PasswordSalt;
            var passwordHash = request.Password?.GetSHA512(salt);
            var today = DateTimeOffset.Now;
            var isPasswordExpired = today.Subtract(adminUser[0].LastPasswordChangeDateTime).Days > 90;

            if (!passwordHash.SequenceEqual(adminUser[0].Password))
            {
                adminUser[0].NumberOfInvalidPasswordAttemps = Math.Min(adminUser[0].NumberOfInvalidPasswordAttemps + 1, int.MaxValue);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //TODO https://docs.microsoft.com/en-us/ef/core/saving/concurrency
                }
                catch (Exception)
                {
                    //TODO
                    throw;
                }
                //TODO:  Auth exception
                throw new Exception();
            }

            return new AdminUserAuthenticateModel(id: adminUser[0].Id,
                                           username: adminUser[0].Username,
                                           name: adminUser[0].Name,
                                           tenantId: adminUser[0].TenantId,
                                           isPasswordExpired: isPasswordExpired);
        }
    }
}
