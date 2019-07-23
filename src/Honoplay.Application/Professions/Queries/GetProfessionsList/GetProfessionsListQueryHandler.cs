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

namespace Honoplay.Application.Professions.Queries.GetProfessionsList
{
    public class GetProfessionsListQueryHandler : IRequestHandler<GetProfessionsListQuery, ResponseModel<ProfessionsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetProfessionsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ProfessionsListModel>> Handle(GetProfessionsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"ProfessionsByTenantId{request.TenantId}";
            var professionsQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Professions
                     .Where(x => x.TenantId == request.TenantId)
                     .AsNoTracking()
                , cancellationToken);

            if (!professionsQuery.Any())
            {
                throw new NotFoundException();
            }

            var professionsList = await professionsQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(ProfessionsListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<ProfessionsListModel>(numberOfTotalItems: professionsQuery.LongCount(), numberOfSkippedItems: request.Skip, source: professionsList);

        }
    }
}
