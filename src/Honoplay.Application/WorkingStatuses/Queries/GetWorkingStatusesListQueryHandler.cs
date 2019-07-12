using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            var allWorkingStatusesList = _context.WorkingStatuses.Where(x => x.TenantId == request.TenantId)
                .AsNoTracking()
                .Select(WorkingStatusesListModel.Projection)
                .ToList();

            var workingStatusesList = await _cacheService.RedisCacheAsync<IList<WorkingStatusesListModel>>(redisKey: redisKey, redisLogic: delegate
                {
                    return allWorkingStatusesList;
                }, cancellationToken: cancellationToken);

            if (!workingStatusesList.Any())
            {
                throw new NotFoundException();
            }

            workingStatusesList = workingStatusesList
                .OrderBy(x => x.Name)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            return new ResponseModel<WorkingStatusesListModel>(numberOfTotalItems: allWorkingStatusesList.Count,
                numberOfSkippedItems: request.Skip,
                source: workingStatusesList);
        }
    }
}
