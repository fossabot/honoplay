using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup
{
    public class CreateTraineeGroupCommand : IRequest<ResponseModel<List<CreateTraineeGroupModel>>>
    {
        public List<CreateTraineeGroupCommandModel> CreateTraineeGroupCommandModels { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateTraineeGroupCommandModel
    {
        public string Name { get; set; }
    }
}