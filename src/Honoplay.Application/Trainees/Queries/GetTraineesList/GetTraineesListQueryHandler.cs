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

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListQueryHandler : IRequestHandler<GetTraineesListQuery, ResponseModel<TraineesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineesListModel>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";

            var traineesQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainees
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            var traineesList = await traineesQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TraineesListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);


            if (!traineesList.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineesListModel>(numberOfTotalItems: traineesQuery.LongCount(),
                                                        numberOfSkippedItems: request.Skip,
                                                        source: traineesList);


        }
    }
}
