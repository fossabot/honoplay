﻿using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListQueryHandler : IRequestHandler<GetTraineesListQuery, ResponseModel<TraineesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineesListModel>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";

            var allTrainees = await _cacheService.RedisCacheAsync<IList<TraineesListModel>>(redisKey, delegate
            {
                return _context.Trainees
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Select(TraineesListModel.Projection)
                    .ToList();

            }, cancellationToken);

            var filteredTrainees = allTrainees
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            if (!filteredTrainees.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineesListModel>(numberOfTotalItems: allTrainees.Count, numberOfSkippedItems: request.Skip, source: filteredTrainees);


        }
    }
}
