using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQuery : IRequest<ResponseModel<TraineeDetailModel>>
    {
        public GetTraineeDetailQuery(int adminUserId, int id, string hostName)
        {
            Id = id;
            AdminUserId = adminUserId;
            HostName = hostName;
        }

        public GetTraineeDetailQuery()
        {
        }

        public int Id { get; private set; }
        public int AdminUserId { get; private set; }
        public string HostName { get; private set; }
    }
}