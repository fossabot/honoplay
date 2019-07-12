using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var professionsList = await _cacheService.RedisCacheAsync<IList<ProfessionsListModel>>(redisKey, delegate
            {
                return _context.Professions
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                    .Select(ProfessionsListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!professionsList.Any())
            {
                throw new NotFoundException();
            }

            professionsList = professionsList
                .OrderBy(x => x.Name)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<ProfessionsListModel>(numberOfTotalItems: professionsList.Count, numberOfSkippedItems: request.Skip, source: professionsList);

        }
    }
}
