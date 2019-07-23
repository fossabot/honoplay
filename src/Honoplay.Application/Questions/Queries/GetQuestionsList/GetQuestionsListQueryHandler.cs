using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryHandler : IRequestHandler<GetQuestionsListQuery, ResponseModel<QuestionsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionsListModel>> Handle(GetQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionsByTenantId{request.TenantId}";
            var questionsQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Questions
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            if (!questionsQuery.Any())
            {
                throw new NotFoundException();
            }

            var questionsList = await questionsQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(QuestionsListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<QuestionsListModel>(numberOfTotalItems: questionsQuery.LongCount(), numberOfSkippedItems: request.Skip, source: questionsList);

        }
    }
}
