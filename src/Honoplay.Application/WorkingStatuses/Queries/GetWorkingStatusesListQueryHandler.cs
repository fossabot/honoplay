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

namespace Honoplay.Application.WorkingStatuses.Queries
{
    public class GetWorkingStatusesListQueryHandler : IRequestHandler<GetWorkingStatusesListQuery, ResponseModel<WorkingStatusesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetWorkingStatusesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<WorkingStatusesListModel>> Handle(GetWorkingStatusesListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"WorkingStatusesByTenantId{request.TenantId}";

            var workingStatusesQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.WorkingStatuses
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            if (!workingStatusesQuery.Any())
            {
                throw new NotFoundException();
            }

            var workingStatusesList = await workingStatusesQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .OrderBy(x => x.Id)
                .Select(WorkingStatusesListModel.Projection)
                .ToListAsync(cancellationToken);

            return new ResponseModel<WorkingStatusesListModel>(numberOfTotalItems: workingStatusesQuery.LongCount(),
                                                               numberOfSkippedItems: request.Skip,
                                                               source: workingStatusesList);
        }
    }
}
