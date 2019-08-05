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

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListQueryHandler : IRequestHandler<GetClassroomsListQuery, ResponseModel<ClassroomsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetClassroomsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ClassroomsListModel>> Handle(GetClassroomsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"ClassroomsByTenantId{request.TenantId}";
            var allClassroomsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Classrooms
                    .Include(x => x.Training)
                    .Include(x => x.Training.TrainingSeries)
                    .AsNoTracking()
                    .Where(x => x.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allClassroomsList.Any())
            {
                throw new NotFoundException();
            }

            var classroomsList = allClassroomsList
                .Select(ClassroomsListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<ClassroomsListModel>(numberOfTotalItems: classroomsList.Count, numberOfSkippedItems: request.Skip, source: classroomsList);

        }
    }
}
