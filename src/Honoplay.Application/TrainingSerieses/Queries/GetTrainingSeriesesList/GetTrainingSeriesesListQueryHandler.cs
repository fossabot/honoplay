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

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public class GetTrainingSeriesesListQueryHandler : IRequestHandler<GetTrainingSeriesesListQuery, ResponseModel<TrainingSeriesesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingSeriesesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingSeriesesListModel>> Handle(GetTrainingSeriesesListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingSeriesesByTenantId{request.TenantId}";
            var allTrainingSeriesesList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainingSerieses
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                , cancellationToken);

            if (!allTrainingSeriesesList.Any())
            {
                throw new NotFoundException();
            }

            var trainingSeriesesList = allTrainingSeriesesList
                .Select(TrainingSeriesesListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<TrainingSeriesesListModel>(numberOfTotalItems: trainingSeriesesList.Count, numberOfSkippedItems: request.Skip, source: trainingSeriesesList);

        }
    }
}
