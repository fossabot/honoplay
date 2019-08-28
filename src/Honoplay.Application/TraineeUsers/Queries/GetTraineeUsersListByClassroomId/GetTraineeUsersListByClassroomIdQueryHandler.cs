using Honoplay.Application._Infrastructure;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersListByClassroomId
{
    public class GetTraineeUsersListByClassroomIdQueryHandler : IRequestHandler<GetTraineeUsersListByClassroomIdQuery, ResponseModel<GetTraineeUsersListByClassroomIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeUsersListByClassroomIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<GetTraineeUsersListByClassroomIdModel>> Handle(GetTraineeUsersListByClassroomIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeUsersWithDepartmentsByTenantId{request.TenantId}";

            var redisTraineeUsers = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeUsers
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                , cancellationToken);

            var traineeUsers = redisTraineeUsers
                .Include(x => x.ClassroomTraineeUsers)
                .SelectMany(x => x.ClassroomTraineeUsers.Where(y => y.ClassroomId == request.ClassroomId))
                .Include(x => x.TraineeUser)
                .Select(x => x.TraineeUser)
                .Select(GetTraineeUsersListByClassroomIdModel.Projection);

            return new ResponseModel<GetTraineeUsersListByClassroomIdModel>(numberOfTotalItems: traineeUsers.LongCount(), numberOfSkippedItems: 0, source: traineeUsers);
        }
    }
}