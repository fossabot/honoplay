using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.Trainees.Queries.GetTraineeList
{
    public class GetTraineesListQuery : IRequest<ResponseModel<TraineesListModel>>
    {
        public GetTraineesListQuery()
        {
        }

        public GetTraineesListQuery(int adminUserId, string hostName, int skip = 0, int take = 10)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
            HostName = hostName;
        }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public string HostName { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTraineesListQueryModel : IRequest<ResponseModel<TraineesListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
