using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQuery : IRequest<ResponseModel<TraineeDetailModel>>
    {
        public GetTraineeDetailQuery(int id, int adminUserId, string hostName)
        {
            Id = id;
            AdminUserId = adminUserId;
            HostName = hostName;
        }

        public GetTraineeDetailQuery()
        {
        }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public string HostName { get; private set; }
    }
}