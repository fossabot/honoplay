using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public class UpdateTrainingSeriesCommand : IRequest<ResponseModel<UpdateTrainingSeriesModel>>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public string Name { get; set; }
    }
}
