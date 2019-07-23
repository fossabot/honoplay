using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, ResponseModel<CreateAnswerModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateAnswerCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<CreateAnswerModel>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"AnswersWithQuestionByTenantId{request.TenantId}";
            var newAnswer = new Answer
            {
                CreatedBy = request.CreatedBy,
                OrderBy = request.OrderBy,
                QuestionId = request.QuestionId,
                Text = request.Text
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var questionIsExist = await _context.Questions
                        .AnyAsync(x =>
                            x.Id == request.QuestionId,
                            cancellationToken);

                    if (!questionIsExist)
                    {
                        throw new NotFoundException(nameof(Question), request.QuestionId);
                    }

                    await _context.Answers.AddAsync(newAnswer, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    
                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Answers
                            .AsNoTracking()
                            .Include(x => x.Question)
                            .ToList()
                        , cancellationToken);
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
            var createdAnswer = new CreateAnswerModel(newAnswer.Id,
                                                      newAnswer.Text,
                                                      newAnswer.OrderBy,
                                                      newAnswer.QuestionId,
                                                      newAnswer.CreatedBy,
                                                      newAnswer.CreatedAt);

            return new ResponseModel<CreateAnswerModel>(createdAnswer);
        }
    }
}
