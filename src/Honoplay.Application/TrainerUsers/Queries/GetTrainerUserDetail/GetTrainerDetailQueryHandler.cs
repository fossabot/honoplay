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

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUserDetail
{
    public class GetTrainerUserDetailQueryHandler : IRequestHandler<GetTrainerUserDetailQuery, ResponseModel<TrainerUserDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainerUserDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainerUserDetailModel>> Handle(GetTrainerUserDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainerUsersWithDepartmentsByTenantId{request.TenantId}";

            var redisTrainerUsers = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainerUsers
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                , cancellationToken);

            var trainerUser = redisTrainerUsers.FirstOrDefault(x => x.Id == request.Id);

            if (trainerUser is null)
            {
                throw new NotFoundException(nameof(TrainerUser), request.Id);
            }

            var model = TrainerUserDetailModel.Create(trainerUser);
            return new ResponseModel<TrainerUserDetailModel>(model);
        }
    }
}