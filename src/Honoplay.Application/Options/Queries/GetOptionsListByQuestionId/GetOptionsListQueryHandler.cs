using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Options.Queries.GetOptionsListByQuestionId
{
    public class GetOptionsListByQuestionIdQueryHandler : IRequestHandler<GetOptionsListByQuestionIdQuery, ResponseModel<OptionsListByQuestionIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetOptionsListByQuestionIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<OptionsListByQuestionIdModel>> Handle(GetOptionsListByQuestionIdQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var allOptionsListByQuestionId = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Options
                    .Include(x => x.Question)
                    .AsNoTracking()
                    .Where(x => x.Question.TenantId == request.TenantId)
                , cancellationToken);

            if (!allOptionsListByQuestionId.Any())
            {
                throw new NotFoundException();
            }

            var optionsListByQuestionId = allOptionsListByQuestionId
                .Where(x => x.QuestionId == request.QuestionId)
                .Select(OptionsListByQuestionIdModel.Projection)
                .ToList();

            return new ResponseModel<OptionsListByQuestionIdModel>(numberOfTotalItems: optionsListByQuestionId.Count, numberOfSkippedItems: 0, source: optionsListByQuestionId);

        }
    }
}
