using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Options.Queries.GetOptionsList
{
    public class GetOptionsListQueryHandler : IRequestHandler<GetOptionsListQuery, ResponseModel<OptionsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetOptionsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<OptionsListModel>> Handle(GetOptionsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var allOptionsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Options
                    .Include(x => x.Question)
                    .AsNoTracking()
                    .Where(x => x.Question.TenantId == request.TenantId)
                , cancellationToken);

            if (!allOptionsList.Any())
            {
                throw new NotFoundException();
            }

            var optionsList = allOptionsList
                .Select(OptionsListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<OptionsListModel>(numberOfTotalItems: optionsList.Count, numberOfSkippedItems: request.Skip, source: optionsList);

        }
    }
}
