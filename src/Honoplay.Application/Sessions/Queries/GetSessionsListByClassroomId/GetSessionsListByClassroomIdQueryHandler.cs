using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId
{
    public class GetSessionsListByClassroomIdQueryHandler : IRequestHandler<GetSessionsListByClassroomIdQuery, ResponseModel<SessionsListByClassroomIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetSessionsListByClassroomIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<SessionsListByClassroomIdModel>> Handle(GetSessionsListByClassroomIdQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"SessionsByTenantId{request.TenantId}";
            var allSessionsListByClassroomId = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Sessions
                    .AsNoTracking()
                    .Include(x => x.Classroom)
                    .Include(x => x.Classroom.Training)
                    .Include(x => x.Classroom.Training.TrainingSeries)
                    .Where(x => x.Classroom.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allSessionsListByClassroomId.Any())
            {
                throw new NotFoundException();
            }

            var sessionsListByClassroomId = allSessionsListByClassroomId
                .Where(x => x.ClassroomId == request.ClassroomId)
                .Select(SessionsListByClassroomIdModel.Projection)
                .ToList();

            return new ResponseModel<SessionsListByClassroomIdModel>(numberOfTotalItems: sessionsListByClassroomId.Count, numberOfSkippedItems: 0, source: sessionsListByClassroomId);

        }
    }
}
