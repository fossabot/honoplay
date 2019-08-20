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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, ResponseModel<List<CreateOptionModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateOptionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateOptionModel>>> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var newOptions = new List<Option>();
            var createdOptions = new List<CreateOptionModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var currentQuestions = _context.Questions.Where(x => x.TenantId == request.TenantId);

                    foreach (var createOptionModel in request.CreateOptionModels)
                    {
                        var questionIsExist = await currentQuestions
                            .AnyAsync(x =>
                                    x.Id == createOptionModel.QuestionId,
                                cancellationToken);

                        if (!questionIsExist)
                        {
                            throw new NotFoundException(nameof(Question), createOptionModel.QuestionId);
                        }
                        var newOption = new Option
                        {
                            CreatedBy = request.CreatedBy,
                            VisibilityOrder = createOptionModel.VisibilityOrder,
                            QuestionId = createOptionModel.QuestionId,
                            Text = createOptionModel.Text,
                            IsCorrect = createOptionModel.IsCorrect
                        };
                        newOptions.Add(newOption);
                    }

                    if (newOptions.Count > 20)
                    {
                        _context.BulkInsert(newOptions);
                    }
                    else
                    {
                        await _context.Options.AddRangeAsync(newOptions, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Options
                                 .AsNoTracking()
                                 .Include(x => x.Question)
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newOptions.ForEach(x => createdOptions.Add(new CreateOptionModel(x.Id,
                                                                                     x.Text,
                                                                                     x.VisibilityOrder,
                                                                                     x.QuestionId,
                                                                                     x.CreatedBy,
                                                                                     x.CreatedAt,
                                                                                     x.IsCorrect)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(Option), ExceptionMessageExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<CreateOptionModel>>(createdOptions);
        }
    }
}
