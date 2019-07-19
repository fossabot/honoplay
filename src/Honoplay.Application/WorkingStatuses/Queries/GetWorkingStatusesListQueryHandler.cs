using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
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

            var allWorkingStatusesList = await _cacheService.RedisCacheAsync(redisKey: redisKey, redisLogic: delegate
            {
                return _context.WorkingStatuses
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking();
            }, cancellationToken: cancellationToken);

            if (!allWorkingStatusesList.Any())
            {
                throw new NotFoundException();
            }

            var workingStatusesList = allWorkingStatusesList
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(WorkingStatusesListModel.Projection)
                .OrderBy(x => x.Name)
                .ToList();

            return new ResponseModel<WorkingStatusesListModel>(numberOfTotalItems: allWorkingStatusesList.ToList().Count,
                                                               numberOfSkippedItems: request.Skip,
                                                               source: workingStatusesList);
        }
    }
}
