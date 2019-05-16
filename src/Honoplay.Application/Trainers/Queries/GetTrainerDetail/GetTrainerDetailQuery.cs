using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQuery : IRequest<ResponseModel<TrainerDetailModel>>
    {
        public GetTrainerDetailQuery(int adminUserId, int id)
        {
            Id = id;
            AdminUserId = adminUserId;
        }

        public GetTrainerDetailQuery()
        {

        }

        public int Id { get; private set; }

        public int AdminUserId { get; private set; }
    }
}
