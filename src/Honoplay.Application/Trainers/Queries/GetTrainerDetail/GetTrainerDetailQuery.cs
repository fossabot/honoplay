using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQuery : IRequest<ResponseModel<TrainerDetailModel>>
    {
        public GetTrainerDetailQuery(int adminUserId, int id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTrainerDetailQuery()
        {

        }

        public int Id { get; private set; }
        public Guid TenantId { get; private set; }
        public int AdminUserId { get; private set; }
    }
}
