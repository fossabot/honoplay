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

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQueryHandler : IRequestHandler<GetTrainersListQuery, ResponseModel<TrainersListModel>>
    {

        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<TrainersListModel>> Handle(GetTrainersListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByTenantId{request.TenantId}";
            var trainersQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainers
                    .Include(t => t.Department)
                    .Where(t => t.Department.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            if (!trainersQuery.Any())
            {
                throw new NotFoundException();
            }

            var trainersList = await trainersQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TrainersListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<TrainersListModel>(numberOfTotalItems: trainersQuery.LongCount(), numberOfSkippedItems: request.Take, source: trainersList);
        }
    }
}