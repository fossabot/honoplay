using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, ResponseModel<CreateOptionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateOptionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<CreateOptionModel>> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var newOption = new Option
            {
                CreatedBy = request.CreatedBy,
                VisibilityOrder = request.VisibilityOrder,
                QuestionId = request.QuestionId,
                Text = request.Text,
                IsCorrect = request.IsCorrect
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

                    await _context.Options.AddAsync(newOption, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        delegate
                        {
                            return _context.Options
                                .AsNoTracking()
                                .Include(x => x.Question)
                                .ToListAsync(cancellationToken);
                        },
                        cancellationToken);

                    transaction.Commit();
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
            var createdOption = new CreateOptionModel(newOption.Id,
                                                      newOption.Text,
                                                      newOption.VisibilityOrder,
                                                      newOption.QuestionId,
                                                      newOption.CreatedBy,
                                                      newOption.CreatedAt,
                                                      newOption.IsCorrect);

            return new ResponseModel<CreateOptionModel>(createdOption);
        }
    }
}
