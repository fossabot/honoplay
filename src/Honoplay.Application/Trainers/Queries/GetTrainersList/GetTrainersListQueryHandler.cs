using Honoplay.Application._Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQueryHandler : IRequestHandler<GetTrainersListQuery, ResponseModel<TrainersListModel>>
    {
        private readonly HonoplayDbContext _context;
        public GetTrainersListQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<TrainersListModel>> Handle(GetTrainersListQuery request, CancellationToken cancellationToken)
        {

            var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                                     x.AdminUserId == request.AdminUserId &&
                                     x.TenantId == request.TenantId,
                                     cancellationToken);

            var query = _context.Trainers.Where(x => isExist).AsNoTracking().OrderBy(x => x.Name);
            var result = query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(TrainersListModel.Projection)
                .ToList();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TrainersListModel>(numberOfTotalItems: await query.CountAsync(cancellationToken), numberOfSkippedItems: request.Take, source: result);
        }
    }
}
