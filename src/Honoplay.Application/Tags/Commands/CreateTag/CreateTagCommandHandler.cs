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

namespace Honoplay.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ResponseModel<List<CreateTagModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateTagCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateTagModel>>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TagsByTenantId{request.TenantId}";
            var newTags = new List<Tag>();
            var createdTags = new List<CreateTagModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createTagModel in request.CreateTagModels)
                    {
                        var newTag = new Tag
                        {
                            CreatedBy = request.CreatedBy,
                            Name = createTagModel.Name,
                            ToQuestion = createTagModel.ToQuestion
                        };
                        newTags.Add(newTag);
                    }

                    if (newTags.Count > 20)
                    {
                        _context.BulkInsert(newTags);
                    }
                    else
                    {
                        await _context.Tags.AddRangeAsync(newTags, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Tags
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newTags.ForEach(x => createdTags.Add(new CreateTagModel(x.Id,
                                                                            x.Name,
                                                                            x.ToQuestion,
                                                                            x.CreatedBy,
                                                                            x.CreatedAt)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(Tag), ExceptionExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<CreateTagModel>>(createdTags);
        }
    }
}
