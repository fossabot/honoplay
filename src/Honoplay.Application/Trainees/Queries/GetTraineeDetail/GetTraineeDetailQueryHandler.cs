﻿using System.Linq;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;

namespace Honoplay.Application.Tenants.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQueryHandler : IRequestHandler<GetTraineeDetailQuery, ResponseModel<TraineeDetailModel>>
    {
        private readonly HonoplayDbContext _context;

        public GetTraineeDetailQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TraineeDetailModel>> Handle(GetTraineeDetailQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                x.AdminUserId == request.AdminUserId, cancellationToken);

            var trainee = await _context.Trainees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id && isExist, cancellationToken);

            if (trainee is null)
            {
                throw new NotFoundException(nameof(Tenant), request.Id);
            }

            var model = TraineeDetailModel.Create(trainee);
            return new ResponseModel<TraineeDetailModel>(model);
        }
    }
}