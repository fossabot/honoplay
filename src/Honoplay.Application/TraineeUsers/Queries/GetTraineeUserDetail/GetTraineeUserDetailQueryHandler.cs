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

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUserDetail
{
    public class GetTraineeUserDetailQueryHandler : IRequestHandler<GetTraineeUserDetailQuery, ResponseModel<TraineeUserDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeUserDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeUserDetailModel>> Handle(GetTraineeUserDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeUsersWithDepartmentsByTenantId{request.TenantId}";

            var redisTraineeUsers = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeUsers
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Id == request.Id 
                                && x.Department.TenantId == request.TenantId)
                , cancellationToken);

            var traineeUser = redisTraineeUsers.FirstOrDefault(x => x.Id == request.Id);

            if (traineeUser is null)
            {
                throw new NotFoundException(nameof(TraineeUser), request.Id);
            }

            var model = TraineeUserDetailModel.Create(traineeUser);
            return new ResponseModel<TraineeUserDetailModel>(model);
        }
    }
}