using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Tenants.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQuery : IRequest<ResponseModel<TraineeDetailModel>>
    {
        public GetTraineeDetailQuery(int adminUserId, int id)
        {
            Id = id;
            AdminUserId = adminUserId;
        }

        public GetTraineeDetailQuery()
        {
        }

        public int Id { get; private set; }
        public int AdminUserId { get; private set; }
    }
}