using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries
{
    public class CreateTrainingSeriesCommand : IRequest<ResponseModel<List<CreateTrainingSeriesModel>>>
    {
        public List<CreateTrainingSeriesCommandModel> CreateTrainingSeriesModels { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateTrainingSeriesCommandModel
    {
        public string Name { get; set; }
    }
}
