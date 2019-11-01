﻿using Honoplay.Application._Infrastructure;
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

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, ResponseModel<UpdateQuestionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public UpdateQuestionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateQuestionModel>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionsByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;

            var questionListByTenantId = _context.Questions
                .Where(x => x.TenantId == request.TenantId)
                .Include(x => x.QuestionTags)
                .ThenInclude(y => y.Tag);

            var updateQuestion = questionListByTenantId.FirstOrDefault(x => x.Id == request.Id);

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    if (updateQuestion == null)
                    {
                        throw new NotFoundException(nameof(Question), request.Id);
                    }

                    updateQuestion.Duration = request.Duration;
                    updateQuestion.Text = request.Text;
                    updateQuestion.QuestionCategoryId = request.CategoryId;
                    updateQuestion.QuestionDifficultyId = request.DifficultyId;
                    updateQuestion.QuestionTypeId = request.TypeId;
                    updateQuestion.ContentFileId = request.ContentFileId;
                    updateQuestion.UpdatedAt = updatedAt;
                    updateQuestion.UpdatedBy = request.UpdatedBy;


                    if (request.TagsIdList != null)
                    {
                        _context.QuestionTags.RemoveRange(updateQuestion.QuestionTags);
                        foreach (var TagId in request.TagsIdList)
                        {
                            updateQuestion.QuestionTags.Add(new QuestionTag
                            {
                                QuestionId = updateQuestion.Id,
                                TagId = TagId
                            });
                        }

                    }


                    _context.Questions.Update(updateQuestion);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => questionListByTenantId.ToList()
                        , cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Question), request.Text);
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

            var updateQuestionModel = new UpdateQuestionModel(updateQuestion.Id,
                                                              updateQuestion.Text,
                                                              updateQuestion.Duration,
                                                              updateQuestion.QuestionDifficultyId,
                                                              updateQuestion.QuestionCategoryId,
                                                              updateQuestion.QuestionTypeId,
                                                              updateQuestion.QuestionTags.Select(x => TagDetailModel.Create(x.Tag)).ToList(),
                                                              updateQuestion.ContentFileId,
                                                              request.UpdatedBy,
                                                              updatedAt);
            return new ResponseModel<UpdateQuestionModel>(updateQuestionModel);
        }
    }
}
