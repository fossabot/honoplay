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



namespace Honoplay.Application.AdminUsers.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, ResponseModel<UpdateAdminUserModel>>
    {
        private readonly HonoplayDbContext _context;

        public UpdateAdminUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<UpdateAdminUserModel>> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.AdminUsers.SingleOrDefaultAsync(au => au.Id == request.Id, cancellationToken);
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
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var model = new UpdateAdminUserModel(id: item.Id,
                email: item.Email,
                username: item.UserName,
                name: item.Name,
                surname: item.Surname,
                phoneNumber: item.PhoneNumber,
                timeZone: item.TimeZone,
                createDateTime: item.CreatedDateTime);

            return new ResponseModel<UpdateAdminUserModel>(model);
        }
    }
}