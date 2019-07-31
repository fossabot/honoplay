using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Trainings.Commands.CreateTraining
{
    public class CreateTrainingCommand : IRequest<ResponseModel<List<CreateTrainingModel>>>
    {
        public List<CreateTrainingCommandModel> CreateTrainingModels { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateTrainingCommandModel
    {
        public int TrainingSeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
