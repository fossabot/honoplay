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

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryCommandHandler : IRequestHandler<CreateQuestionCategoryCommand, ResponseModel<List<CreateQuestionCategoryModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateQuestionCategoryCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateQuestionCategoryModel>>> Handle(CreateQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionCategoriesByTenantId{request.TenantId}";
            var newQuestionCategories = new List<QuestionCategory>();
            var createdQuestionCategories = new List<CreateQuestionCategoryModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    foreach (var createQuestionCategoryModel in request.CreateQuestionCategoryModels)
                    {
                        var newQuestionCategory = new QuestionCategory
                        {
                            CreatedBy = request.CreatedBy,
                            Name = createQuestionCategoryModel.Name,
                            TenantId = request.TenantId
                        };
                        newQuestionCategories.Add(newQuestionCategory);
                    }

                    if (newQuestionCategories.Count > 20)
                    {
                        _context.BulkInsert(newQuestionCategories);
                    }
                    else
                    {
                        await _context.QuestionCategories.AddRangeAsync(newQuestionCategories, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.QuestionCategories
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newQuestionCategories.ForEach(x => createdQuestionCategories.Add(new CreateQuestionCategoryModel(x.Id,
                                                                                                    x.Name,
                                                                                                    x.CreatedBy,
                                                                                                    x.CreatedAt)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(QuestionCategory), ExceptionExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<CreateQuestionCategoryModel>>(createdQuestionCategories);
        }
    }
}
