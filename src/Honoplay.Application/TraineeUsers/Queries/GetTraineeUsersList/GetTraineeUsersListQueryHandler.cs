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

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList
{
    public class GetTraineeUsersListQueryHandler : IRequestHandler<GetTraineeUsersListQuery, ResponseModel<TraineeUsersListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeUsersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeUsersListModel>> Handle(GetTraineeUsersListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeUsersWithDepartmentsByTenantId{request.TenantId}";

            var traineeUsersQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeUsers
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            var traineeUsersList = await traineeUsersQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TraineeUsersListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);


            if (!traineeUsersList.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineeUsersListModel>(numberOfTotalItems: traineeUsersQuery.LongCount(),
                                                        numberOfSkippedItems: request.Skip,
                                                        source: traineeUsersList);


        }
    }
}
