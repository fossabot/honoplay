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

namespace Honoplay.Application.Sessions.Queries.GetSessionDetail
{
    public class GetSessionDetailQueryHandler : IRequestHandler<GetSessionDetailQuery, ResponseModel<SessionDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetSessionDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<SessionDetailModel>> Handle(GetSessionDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"SessionsByTenantId{request.TenantId}";
            var redisSessions = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Sessions
                    .AsNoTracking()
                    .Include(x => x.Classroom)
                    .Include(x => x.Classroom.Training)
                    .Include(x => x.Classroom.Training.TrainingSeries)
                    .Where(x => x.Classroom.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            var session = redisSessions.FirstOrDefault(x => x.Id == request.Id);

            if (session is null)
            {
                throw new NotFoundException(nameof(Session), request.Id);
            }

            var model = SessionDetailModel.Create(session);
            return new ResponseModel<SessionDetailModel>(model);
        }
    }
}