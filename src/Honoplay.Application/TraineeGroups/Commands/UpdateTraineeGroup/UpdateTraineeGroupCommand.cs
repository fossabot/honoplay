using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupCommand : IRequest<ResponseModel<List<UpdateTraineeGroupModel>>>
    {
        public List<UpdateTraineeGroupCommandModel> UpdateTraineeGroupCommandModels { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class UpdateTraineeGroupCommandModel
    {
        public string Name { get; set; }
    }
}