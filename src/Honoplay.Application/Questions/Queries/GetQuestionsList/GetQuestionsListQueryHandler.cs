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
            var questionsList = await _cacheService.RedisCacheAsync<IList<QuestionsListModel>>(redisKey, delegate
            {
                return _context.Questions
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                    .Select(QuestionsListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!questionsList.Any())
            {
                throw new NotFoundException();
            }

            questionsList = questionsList
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<QuestionsListModel>(numberOfTotalItems: questionsList.Count, numberOfSkippedItems: request.Skip, source: questionsList);

        }
    }
}
