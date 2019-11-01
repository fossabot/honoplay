using Honoplay.Application._Infrastructure;
using Honoplay.Application.Tags.Queries.GetTagDetail;
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
                Duration = request.Duration,
                QuestionCategoryId = request.CategoryId,
                QuestionDifficultyId = request.DifficultyId,
                QuestionTypeId = request.TypeId,
                ContentFileId = request.ContentFileId
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.Questions.AddAsync(newQuestion, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    if (request.TagsIdList != null)
                    {
                        foreach (var tagId in request.TagsIdList)
                        {
                            var questionTag = new QuestionTag
                            {
                                QuestionId = newQuestion.Id,
                                TagId = tagId
                            };
                            await _context.QuestionTags.AddAsync(questionTag, cancellationToken);
                        }
                    }
                    newQuestion.QuestionTags = await _context.QuestionTags
                                         .AsNoTracking()
                                         .Where(x => x.QuestionId == newQuestion.Id)
                                         .Include(x => x.Tag)
                                         .ToListAsync(cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Questions
                            .AsNoTracking()
                            .Where(x => x.TenantId == request.TenantId)
                            .Include(x => x.QuestionTags)
                            .ThenInclude(y => y.Tag)
                            .ToList()
                        , cancellationToken);
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
                                                              newQuestion.QuestionDifficultyId,
                                                              newQuestion.QuestionCategoryId,
                                                              newQuestion.QuestionTags.Select(x => TagDetailModel.Create(x.Tag)).ToList(),
                                                              newQuestion.QuestionTypeId,
                                                              newQuestion.ContentFileId,
                                                              newQuestion.CreatedBy,
                                                              newQuestion.CreatedAt);
            return new ResponseModel<CreateQuestionModel>(createQuestionModel);
        }
    }
}
