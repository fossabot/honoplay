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
using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{

    public class RegisterAdminUserCommandHandler : IRequestHandler<RegisterAdminUserCommand, AdminUserRegisterModel>
    {
        private readonly HonoplayDbContext _context;


        public RegisterAdminUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }


        public async Task<AdminUserRegisterModel> Handle(RegisterAdminUserCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _context.AdminUsers
                                    .Where(u => u.Email == request.Email)
                                    .ToListAsync();

            if (adminUser.Any())
            {
                throw new DataExistingException(nameof(request.Email), request.Email);
            }

            var item = new AdminUser
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                TimeZone = request.TimeZone,
                CreatedDateTime = DateTimeOffset.Now,
                LastPasswordChangeDateTime = DateTimeOffset.Now,
                Password = new byte[0], // TODO: hash algorithm?
                PasswordSalt = new byte[0] // TODO: hash algorithm?
            };

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new AdminUserRegisterModel(id: item.Id,
                                              email: item.Email,
                                              name: item.Name,
                                              surname: item.Surname);
        }
    }
}
