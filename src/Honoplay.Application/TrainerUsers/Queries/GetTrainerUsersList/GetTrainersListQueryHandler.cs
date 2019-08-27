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

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList
{
    public class GetTrainerUsersListQueryHandler : IRequestHandler<GetTrainerUsersListQuery, ResponseModel<TrainerUsersListModel>>
    {

        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainerUsersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<TrainerUsersListModel>> Handle(GetTrainerUsersListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainerUsersWithDepartmentsByTenantId{request.TenantId}";
            var trainerUsersQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainerUsers
                    .Include(t => t.Department)
                    .Where(t => t.Department.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);
            
            var trainerUsersList = await trainerUsersQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TrainerUsersListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            if (!trainerUsersList.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TrainerUsersListModel>(numberOfTotalItems: trainerUsersQuery.LongCount(),
                                                        numberOfSkippedItems: request.Take,
                                                        source: trainerUsersList);
        }
    }
}