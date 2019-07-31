using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainings.Commands.UpdateTraining
{
    public class UpdateTrainingCommand : IRequest<ResponseModel<UpdateTrainingModel>>
    {
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public int TrainingSeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
