using EFCore.BulkExtensions;
using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.ContentFiles.Commands.CreateContentFile
{
    public class CreateContentFileCommandHandler : IRequestHandler<CreateContentFileCommand, ResponseModel<List<CreateContentFileModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateContentFileCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateContentFileModel>>> Handle(CreateContentFileCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"ContentFilesByTenantId{request.TenantId}";
            var newContentFiles = new List<ContentFile>();
            var createdContentFiles = new List<CreateContentFileModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    foreach (var createContentFileModel in request.CreateContentFileModels)
                    {
                        var newContentFile = new ContentFile
                        {
                            CreatedBy = request.CreatedBy,
                            Name = createContentFileModel.Name,
                            ContentType = createContentFileModel.ContentType,
                            Data = createContentFileModel.Data,
                            TenantId = request.TenantId
                        };
                        newContentFiles.Add(newContentFile);
                    }

                    if (newContentFiles.Count > 20)
                    {
                        _context.BulkInsert(newContentFiles);
                    }
                    else
                    {
                        await _context.ContentFiles.AddRangeAsync(newContentFiles, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.ContentFiles
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newContentFiles.ForEach(x => createdContentFiles.Add(new CreateContentFileModel(x.Id,
                                                                                                    x.Name,
                                                                                                    x.ContentType,
                                                                                                    x.Data,
                                                                                                    x.CreatedBy,
                                                                                                    x.CreatedAt)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(ContentFile), ExceptionExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<CreateContentFileModel>>(createdContentFiles);
        }
    }
}
