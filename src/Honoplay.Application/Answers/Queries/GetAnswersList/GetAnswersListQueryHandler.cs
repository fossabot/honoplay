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

namespace Honoplay.Application.Answers.Queries.GetAnswersList
{
    public class GetAnswersListQueryHandler : IRequestHandler<GetAnswersListQuery, ResponseModel<AnswersListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetAnswersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<AnswersListModel>> Handle(GetAnswersListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"AnswersWithQuestionByTenantId{request.TenantId}";
            var answersList = await _cacheService.RedisCacheAsync<IList<AnswersListModel>>(redisKey, delegate
            {
                return _context.Answers
                    .AsNoTracking()
                    .Include(x => x.Question)
                    .Where(x => x.Question.TenantId == request.TenantId)
                    .Select(AnswersListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!answersList.Any())
            {
                throw new NotFoundException();
            }

            answersList = answersList
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<AnswersListModel>(numberOfTotalItems: answersList.Count, numberOfSkippedItems: request.Skip, source: answersList);

        }
    }
}
