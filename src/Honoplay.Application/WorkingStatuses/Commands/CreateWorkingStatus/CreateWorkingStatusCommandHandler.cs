using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusCommandHandler : IRequestHandler<CreateWorkingStatusCommand, ResponseModel<CreateWorkingStatusModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateWorkingStatusCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateWorkingStatusModel>> Handle(CreateWorkingStatusCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"WorkingStatusesByHostName{request.HostName}";

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var currentTenant = await _context.Tenants
                        .FirstOrDefaultAsync(x => x.HostName == request.HostName
                            && x.TenantAdminUsers.Any(y => y.AdminUserId == request.CreatedBy),
                            cancellationToken);

                    if (currentTenant == null)
                    {
                        throw new NotFoundException(nameof(Tenant), request.HostName);
                    }

                    var workingStatus = new WorkingStatus
                    {
                        Name = request.Name,
                        CreatedBy = request.CreatedBy,
                        TenantId = currentTenant.Id
                    };

                    await _context.WorkingStatuses.AddAsync(workingStatus, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                        {
                            return _context.WorkingStatuses.Where(x => x.TenantId == currentTenant.Id);
                        }, cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(WorkingStatus), request.Name);
                }
                catch (NotFoundException)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }
            var workingStatusModel = new CreateWorkingStatusModel(request.Name, request.CreatedBy);
            return new ResponseModel<CreateWorkingStatusModel>(workingStatusModel);
        }
    }
}
