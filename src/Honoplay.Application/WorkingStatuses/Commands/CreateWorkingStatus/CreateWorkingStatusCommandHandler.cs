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
            var redisKey = $"WorkingStatusesByTenantId{request.TenantId}";
            var workingStatus = new WorkingStatus
            {
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                TenantId = request.TenantId
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.WorkingStatuses.AddAsync(workingStatus, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.WorkingStatuses
                            .Where(x => x.TenantId == request.TenantId)
                            .ToList()
                        , cancellationToken);
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
            var workingStatusModel = new CreateWorkingStatusModel(workingStatus.Id,
                                                                  workingStatus.Name,
                                                                  workingStatus.CreatedBy,
                                                                  workingStatus.CreatedAt);

            return new ResponseModel<CreateWorkingStatusModel>(workingStatusModel);
        }
    }
}
