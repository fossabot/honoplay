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

namespace Honoplay.Application.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, ResponseModel<UpdateSessionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateSessionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<UpdateSessionModel>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"SessionsByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var sessionsByTenantId = await _context.Sessions
                        .Include(x => x.Classroom)
                        .Include(x => x.Classroom.Training)
                        .Include(x => x.Classroom.Training.TrainingSeries)
                        .Where(x => x.Classroom.Training.TrainingSeries.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);


                    var updateSession = sessionsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateSession is null)
                    {
                        throw new NotFoundException(nameof(Session), request.Id);
                    }

                    updateSession.Name = request.Name;
                    updateSession.UpdatedAt = updatedAt;
                    updateSession.UpdatedBy = request.UpdatedBy;

                    _context.Sessions.Update(updateSession);
                    await _context.SaveChangesAsync(cancellationToken);

                    sessionsByTenantId = sessionsByTenantId.Select(x => new Session
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        UpdatedAt = x.UpdatedAt,
                        GameId = x.GameId,
                        ClassroomId = x.ClassroomId
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => sessionsByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Session), request.Id);
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
            var updateSessionModel = new UpdateSessionModel(request.Id,
                                                            request.GameId,
                                                            request.ClassroomId,
                                                            request.Name,
                                                            request.UpdatedBy,
                                                            updatedAt);

            return new ResponseModel<UpdateSessionModel>(updateSessionModel);
        }
    }
}
