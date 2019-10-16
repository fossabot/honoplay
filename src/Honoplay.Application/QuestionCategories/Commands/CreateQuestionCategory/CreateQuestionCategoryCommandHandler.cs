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

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryCommandHandler : IRequestHandler<CreateQuestionCategoryCommand, ResponseModel<CreateQuestionCategoryModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateQuestionCategoryCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateQuestionCategoryModel>> Handle(CreateQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionCategoriesByTenantId{request.TenantId}";
            var questionCategory = new QuestionCategory
            {
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                TenantId = request.TenantId
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.QuestionCategories.AddAsync(questionCategory, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.QuestionCategories
                            .Where(x => x.TenantId == request.TenantId)
                            .ToList()
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
            var questionCategoryModel = new CreateQuestionCategoryModel(questionCategory.Id,
                                                                  questionCategory.Name,
                                                                  questionCategory.CreatedBy,
                                                                  questionCategory.CreatedAt);

            return new ResponseModel<CreateQuestionCategoryModel>(questionCategoryModel);
        }
    }
}
