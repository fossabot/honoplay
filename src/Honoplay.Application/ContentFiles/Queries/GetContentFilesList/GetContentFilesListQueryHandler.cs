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

namespace Honoplay.Application.ContentFiles.Queries.GetContentFilesList
{
    public class GetContentFilesListQueryHandler : IRequestHandler<GetContentFilesListQuery, ResponseModel<ContentFilesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetContentFilesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ContentFilesListModel>> Handle(GetContentFilesListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"ContentFilesByTenantId{request.TenantId}";
            var contentFilesQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.ContentFiles
                     .Where(x => x.TenantId == request.TenantId)
                     .AsNoTracking()
                , cancellationToken);

            if (!contentFilesQuery.Any())
            {
                throw new NotFoundException();
            }

            var contentFilesList = await contentFilesQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(ContentFilesListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<ContentFilesListModel>(numberOfTotalItems: contentFilesQuery.LongCount(), numberOfSkippedItems: request.Skip, source: contentFilesList);

        }
    }
}
