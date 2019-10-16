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

namespace Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory
{
    public class UpdateQuestionCategoryCommandHandler : IRequestHandler<UpdateQuestionCategoryCommand, ResponseModel<UpdateQuestionCategoryModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateQuestionCategoryCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateQuestionCategoryModel>> Handle(UpdateQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionCategoriesByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var questionCategories = await _context.QuestionCategories
                        .Where(x => x.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var currentQuestionCategory = questionCategories.FirstOrDefault(x => x.Id == request.Id);

                    if (currentQuestionCategory is null)
                    {
                        throw new NotFoundException(nameof(QuestionCategory), request.Id);
                    }

                    currentQuestionCategory.Name = request.Name;
                    currentQuestionCategory.UpdatedBy = request.UpdatedBy;
                    currentQuestionCategory.UpdatedAt = updatedAt;

                    _context.Update(currentQuestionCategory);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => questionCategories
                        , cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(QuestionCategory), request.Name);
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

            var questionCategoryModel = new UpdateQuestionCategoryModel(request.Id,
                                                                  request.Name,
                                                                  request.UpdatedBy,
                                                                  updatedAt);

            return new ResponseModel<UpdateQuestionCategoryModel>(questionCategoryModel);
        }
    }
}
