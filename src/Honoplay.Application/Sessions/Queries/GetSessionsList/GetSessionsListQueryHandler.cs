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

namespace Honoplay.Application.Sessions.Queries.GetSessionsList
{
    public class GetSessionsListQueryHandler : IRequestHandler<GetSessionsListQuery, ResponseModel<SessionsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetSessionsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<SessionsListModel>> Handle(GetSessionsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"SessionsByTenantId{request.TenantId}";
            var allSessionsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Sessions
                    .AsNoTracking()
                    .Include(x => x.Classroom)
                    .Include(x => x.Classroom.Training)
                    .Include(x => x.Classroom.Training.TrainingSeries)
                    .Where(x => x.Classroom.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allSessionsList.Any())
            {
                throw new NotFoundException();
            }

            var sessionsList = allSessionsList
                .Select(SessionsListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<SessionsListModel>(numberOfTotalItems: sessionsList.Count, numberOfSkippedItems: request.Skip, source: sessionsList);

        }
    }
}
