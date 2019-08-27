using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomCommand : IRequest<ResponseModel<UpdateClassroomModel>>
    {
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
    }
}
