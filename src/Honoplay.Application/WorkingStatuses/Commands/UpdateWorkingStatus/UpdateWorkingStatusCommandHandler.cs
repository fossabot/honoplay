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

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusCommandHandler : IRequestHandler<UpdateWorkingStatusCommand, ResponseModel<UpdateWorkingStatusModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateWorkingStatusCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateWorkingStatusModel>> Handle(UpdateWorkingStatusCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"WorkingStatusByHostName{request.HostName}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var workingStatuses = await _context.Tenants
                        .Include(x => x.TenantAdminUsers)
                        .Include(x => x.WorkingStatuses)
                        .Where(x =>
                                x.HostName == request.HostName
                                && x.TenantAdminUsers.Any(y =>
                                    y.AdminUserId == request.UpdatedBy
                                    && y.TenantId == x.Id))
                        .SelectMany(x => x.WorkingStatuses)
                        .ToListAsync(cancellationToken);

                    var currentWorkingStatus = workingStatuses.FirstOrDefault(x => x.Id == request.Id);

                    if (currentWorkingStatus is null)
                    {
                        throw new NotFoundException(nameof(WorkingStatus), request.Id);
                    }

                    currentWorkingStatus.Name = request.Name;
                    currentWorkingStatus.UpdatedBy = request.UpdatedBy;
                    currentWorkingStatus.UpdatedAt = updatedAt;

                    _context.Update(currentWorkingStatus);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                    {
                        return workingStatuses;
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

            var workingStatusModel = new UpdateWorkingStatusModel(id: request.Id,
                name: request.Name,
                hostName: request.HostName,
                updatedBy: request.UpdatedBy,
                updatedAt: updatedAt);

            return new ResponseModel<UpdateWorkingStatusModel>(workingStatusModel);
        }
    }
}
