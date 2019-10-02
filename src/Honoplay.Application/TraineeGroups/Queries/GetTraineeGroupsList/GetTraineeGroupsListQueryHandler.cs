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

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList
{
    public class GetTraineeGroupsListQueryHandler : IRequestHandler<GetTraineeGroupsListQuery, ResponseModel<TraineeGroupsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeGroupsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeGroupsListModel>> Handle(GetTraineeGroupsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeGroupsByTenantId{request.TenantId}";
            var traineeGroupsQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeGroups
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
            , cancellationToken);

            if (!traineeGroupsQuery.Any())
            {
                throw new NotFoundException();
            }

            var traineeGroupsList = await traineeGroupsQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TraineeGroupsListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<TraineeGroupsListModel>(numberOfTotalItems: traineeGroupsQuery.LongCount(), numberOfSkippedItems: request.Skip, source: traineeGroupsList);

        }
    }
}
