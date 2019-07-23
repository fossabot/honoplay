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

namespace Honoplay.Application.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, ResponseModel<UpdateAnswerModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateAnswerCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateAnswerModel>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"AnswersWithQuestionByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var answersByTenantId = await _context.Answers
                        .Include(x => x.Question)
                        .Where(x => x.Question.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);


                    var updateAnswer = answersByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateAnswer is null)
                    {
                        throw new NotFoundException(nameof(Answer), request.Id);
                    }

                    updateAnswer.OrderBy = request.OrderBy;
                    updateAnswer.QuestionId = request.QuestionId;
                    updateAnswer.Text = request.Text;
                    updateAnswer.UpdatedAt = updatedAt;
                    updateAnswer.UpdatedBy = request.UpdatedBy;

                    _context.Answers.Update(updateAnswer);
                    await _context.SaveChangesAsync(cancellationToken);

                    answersByTenantId = answersByTenantId.Select(x => new Answer
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        OrderBy = x.OrderBy,
                        QuestionId = x.QuestionId,
                        Text = x.Text,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => answersByTenantId,
                        cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Answer), request.Id);
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
            var updateAnswerModel = new UpdateAnswerModel(request.Id,
                                                          request.Text,
                                                          request.OrderBy,
                                                          request.QuestionId,
                                                          request.UpdatedBy,
                                                          updatedAt);

            return new ResponseModel<UpdateAnswerModel>(updateAnswerModel);
        }
    }
}
