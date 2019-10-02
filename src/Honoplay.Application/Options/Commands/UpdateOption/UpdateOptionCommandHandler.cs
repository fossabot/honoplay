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

namespace Honoplay.Application.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand, ResponseModel<UpdateOptionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateOptionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateOptionModel>> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var optionsByTenantId = await _context.Options
                        .Include(x => x.Question)
                        .Where(x => x.Question.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);


                    var updateOption = optionsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateOption is null)
                    {
                        throw new NotFoundException(nameof(Option), request.Id);
                    }

                    updateOption.VisibilityOrder = request.VisibilityOrder;
                    updateOption.Text = request.Text;
                    updateOption.UpdatedAt = updatedAt;
                    updateOption.UpdatedBy = request.UpdatedBy;
                    updateOption.IsCorrect = request.IsCorrect;

                    _context.Options.Update(updateOption);
                    await _context.SaveChangesAsync(cancellationToken);

                    optionsByTenantId = optionsByTenantId.Select(x => new Option
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        VisibilityOrder = x.VisibilityOrder,
                        Text = x.Text,
                        UpdatedAt = x.UpdatedAt,
                        IsCorrect = x.IsCorrect
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => optionsByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Option), request.Id);
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
            var updateOptionModel = new UpdateOptionModel(request.Id,
                                                          request.Text,
                                                          request.VisibilityOrder,
                                                          request.QuestionId,
                                                          request.UpdatedBy,
                                                          request.IsCorrect,
                                                          updatedAt);

            return new ResponseModel<UpdateOptionModel>(updateOptionModel);
        }
    }
}
