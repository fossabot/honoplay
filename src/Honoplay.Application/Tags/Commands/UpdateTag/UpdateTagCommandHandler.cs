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

namespace Honoplay.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ResponseModel<UpdateTagModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTagCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateTagModel>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TagsByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var tagsByTenantId = await _context.QuestionTags
                        .Include(x => x.Question)
                        .Include(x => x.Tag)
                        .Where(x => x.Question.TenantId == request.TenantId)
                        .Select(x => x.Tag)
                        .ToListAsync(cancellationToken);

                    var updateTag = tagsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateTag is null)
                    {
                        throw new NotFoundException(nameof(Tag), request.Id);
                    }

                    updateTag.Name = request.Name;
                    updateTag.UpdatedAt = updatedAt;
                    updateTag.UpdatedBy = request.UpdatedBy;
                    updateTag.ToQuestion = request.ToQuestion;

                    _context.Tags.Update(updateTag);
                    await _context.SaveChangesAsync(cancellationToken);

                    tagsByTenantId = tagsByTenantId.Select(x => new Tag
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        UpdatedAt = x.UpdatedAt,
                        ToQuestion = x.ToQuestion
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => tagsByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Tag), request.Id);
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
            var updateTagModel = new UpdateTagModel(request.Id,
                                                    request.Name,
                                                    request.ToQuestion,
                                                    request.UpdatedBy,
                                                    updatedAt);

            return new ResponseModel<UpdateTagModel>(updateTagModel);
        }
    }
}
