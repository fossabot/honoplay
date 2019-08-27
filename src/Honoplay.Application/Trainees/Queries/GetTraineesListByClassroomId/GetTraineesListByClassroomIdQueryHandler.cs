using Honoplay.Application._Infrastructure;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineesListByClassroomId
{
    public class GetTraineesListByClassroomIdQueryHandler : IRequestHandler<GetTraineesListByClassroomIdQuery, ResponseModel<GetTraineesListByClassroomIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineesListByClassroomIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<GetTraineesListByClassroomIdModel>> Handle(GetTraineesListByClassroomIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";

            var redisTrainees = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainees
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                , cancellationToken);

            var trainees = redisTrainees
                .Include(x => x.ClassroomTrainees)
                .SelectMany(x => x.ClassroomTrainees.Where(y => y.ClassroomId == request.ClassroomId))
                .Include(x => x.Trainee)
                .Select(x => x.Trainee)
                .Select(GetTraineesListByClassroomIdModel.Projection);

            return new ResponseModel<GetTraineesListByClassroomIdModel>(numberOfTotalItems: trainees.LongCount(), numberOfSkippedItems: 0, source: trainees);
        }
    }
}