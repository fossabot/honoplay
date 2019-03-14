using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Microsoft.Data.Sqlite;



namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserCommandHandler : IRequestHandler<RegisterAdminUserCommand, ResponseModel<AdminUserRegisterModel>>
    {
        private readonly HonoplayDbContext _context;

        public RegisterAdminUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AdminUserRegisterModel>> Handle(RegisterAdminUserCommand request, CancellationToken cancellationToken)
        {
            var salt = ByteArrayExtensions.GetRandomSalt();
            var item = new AdminUser
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                TimeZone = request.TimeZone,
                CreatedDateTime = DateTimeOffset.Now,
                LastPasswordChangeDateTime = DateTimeOffset.Now,
                PasswordSalt = salt,
                Password = request.Password.GetSHA512(salt),
            };

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(AdminUser), nameof(AdminUser.Email));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var model = new AdminUserRegisterModel(id: item.Id,
                email: item.Email,
                username: item.UserName,
                name: item.Name,
                surname: item.Surname,
                phoneNumber: item.PhoneNumber,
                timeZone: item.TimeZone,
                createDateTime: item.CreatedDateTime);

            return new ResponseModel<AdminUserRegisterModel>(model);
        }
    }
}