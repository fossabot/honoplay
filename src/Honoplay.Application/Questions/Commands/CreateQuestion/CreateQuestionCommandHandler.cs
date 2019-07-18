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

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ResponseModel<CreateQuestionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateQuestionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateQuestionModel>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionsByTenantId{request.TenantId}";

            var newQuestion = new Question
            {
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                Text = request.Text,
                Duration = request.Duration
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.Questions.AddAsync(newQuestion, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                        {
                            return _context.Questions.Where(x => x.TenantId == request.TenantId);
                        }, cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Question), request.Text);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var createQuestionModel = new CreateQuestionModel(newQuestion.Id,
                                                              newQuestion.Text,
                                                              newQuestion.Duration,
                                                              newQuestion.CreatedBy,
                                                              newQuestion.CreatedAt);
            return new ResponseModel<CreateQuestionModel>(createQuestionModel);
        }
    }
}
