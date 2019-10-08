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

namespace Honoplay.Application.ContentFiles.Commands.UpdateContentFile
{
    public class UpdateContentFileCommandHandler : IRequestHandler<UpdateContentFileCommand, ResponseModel<UpdateContentFileModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateContentFileCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateContentFileModel>> Handle(UpdateContentFileCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"ContentFilesByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var contentFilesByTenantId = await _context.ContentFiles
                        .Where(x => x.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var updateContentFile = contentFilesByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateContentFile is null)
                    {
                        throw new NotFoundException(nameof(ContentFile), request.Id);
                    }

                    updateContentFile.Name = request.Name;
                    updateContentFile.ContentType = request.ContentType;
                    updateContentFile.UpdatedAt = updatedAt;
                    updateContentFile.UpdatedBy = request.UpdatedBy;
                    updateContentFile.Data = request.Data;

                    _context.ContentFiles.Update(updateContentFile);
                    await _context.SaveChangesAsync(cancellationToken);

                    contentFilesByTenantId = contentFilesByTenantId.Select(x => new ContentFile
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        ContentType = x.ContentType,
                        UpdatedAt = x.UpdatedAt,
                        Data = x.Data
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => contentFilesByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(ContentFile), request.Id);
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
            var updateContentFileModel = new UpdateContentFileModel(request.Id,
                                                          request.Name,
                                                          request.ContentType,
                                                          request.Data,
                                                          request.UpdatedBy,
                                                          updatedAt);

            return new ResponseModel<UpdateContentFileModel>(updateContentFileModel);
        }
    }
}
