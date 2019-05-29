using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQueryHandler : IRequestHandler<GetTraineeDetailQuery, ResponseModel<TraineeDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly IDistributedCache _cache;

        public GetTraineeDetailQueryHandler(HonoplayDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<ResponseModel<TraineeDetailModel>> Handle(GetTraineeDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByAdminUserId{request.AdminUserId}";
            var serializedRedisTrainees = await _cache.GetStringAsync(redisKey, cancellationToken);
            List<Trainee> redisTrainees;

            if (!string.IsNullOrEmpty(serializedRedisTrainees))
            {
                redisTrainees = JsonConvert.DeserializeObject<List<Trainee>>(serializedRedisTrainees);
            }
            else
            {
                redisTrainees = await _context.Trainees.AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Id == request.Id &&
                                _context.TenantAdminUsers
                                    .Any(y => y.TenantId == x.Department.TenantId &&
                                              y.AdminUserId == request.AdminUserId))
                    .ToListAsync(cancellationToken);

                await _cache.SetStringAsync(redisKey, JsonConvert.SerializeObject(redisTrainees), cancellationToken);
            }
            var trainee = redisTrainees.FirstOrDefault(x => x.Id == request.Id);

            //var trainee = await _context.Trainees.AsNoTracking()
            //    .Include(x => x.Department)
            //    .Where(x => x.Id == request.Id &&
            //                _context.TenantAdminUsers
            //                    .Any(y => y.TenantId == x.Department.TenantId &&
            //                              y.AdminUserId == request.AdminUserId))
            //    .FirstOrDefaultAsync(cancellationToken);

            if (trainee is null)
            {
                throw new NotFoundException(nameof(Trainee), request.Id);
            }

            var model = TraineeDetailModel.Create(trainee);
            return new ResponseModel<TraineeDetailModel>(model);
        }
    }
}