using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQueryHandler : IRequestHandler<GetTraineeDetailQuery, ResponseModel<TraineeDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeDetailModel>> Handle(GetTraineeDetailQuery request, CancellationToken cancellationToken)
        {

            var redisKey = $"TraineesWithDepartmentsByAdminUserId{request.AdminUserId}";
            IList<Trainee> redisTrainees;
            try
            {
                redisTrainees = await _cacheService.RedisCacheAsync<IList<Trainee>>(redisKey, delegate
                {
                    return _context.Trainees.AsNoTracking()
                        .Include(x => x.Department)
                        .Where(x => x.Id == request.Id &&
                                    _context.TenantAdminUsers
                                        .Any(y => y.TenantId == x.Department.TenantId &&
                                                  y.AdminUserId == request.AdminUserId))
                        .ToList();
                }, cancellationToken);
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException(nameof(redisKey), redisKey);
            }
            catch (ApplicationException)
            {
                throw new NotFoundException(nameof(Trainee), request.Id);
            }


            var trainee = redisTrainees.FirstOrDefault(x => x.Id == request.Id);

            if (trainee is null)
            {
                throw new NotFoundException(nameof(Trainee), request.Id);
            }

            var model = TraineeDetailModel.Create(trainee);
            return new ResponseModel<TraineeDetailModel>(model);
        }
    }
}