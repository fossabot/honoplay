using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;


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
                                    .SingleOrDefaultAsync(u => u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase), cancellationToken: cancellationToken);

            if (adminUser is null)
            {
                throw new NotFoundException(nameof(request.Email), request.Email);
            }

            var salt = adminUser.PasswordSalt;
            var passwordHash = request.Password?.GetSHA512(salt);
            var today = DateTimeOffset.Now;
            var isPasswordExpired = today.Subtract(adminUser.LastPasswordChangeDateTime).Days > 90;
            List<Guid> tenants = new List<Guid>();

            if (!passwordHash.SequenceEqual(adminUser.Password))
            {
                adminUser.NumberOfInvalidPasswordAttemps = Math.Min(adminUser.NumberOfInvalidPasswordAttemps + 1, int.MaxValue);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    tenants = _context.TenantAdminUsers.Where(x => x.AdminUserId == adminUser.Id).AsNoTracking().Select(x => x.TenantId).ToList();
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

            return new AdminUserAuthenticateModel(id: adminUser.Id,
                                           email: adminUser.Email,
                                           name: adminUser.Name,
                                           isPasswordExpired: isPasswordExpired,
                                           tenants: tenants);
        }
    }
}