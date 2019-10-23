using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomCommand : IRequest<ResponseModel<List<CreateClassroomModel>>>
    {
        public List<CreateClassroomCommandModel> CreateClassroomModels { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateClassroomCommandModel
    {
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public List<int> TraineeUsersIdList { get; set; }
        public DateTimeOffset BeginDatetime { get; set; }
        public DateTimeOffset EndDatetime { get; set; }
        public string Location { get; set; }
    }
}
