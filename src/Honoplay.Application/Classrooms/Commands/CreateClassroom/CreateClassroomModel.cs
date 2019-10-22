using System;
using System.Collections.Generic;

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public struct CreateClassroomModel
    {
        public int Id { get; }
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public List<int> TraineeUsersIdList { get; set; }
        public DateTimeOffset BeginDatetime { get; set; }
        public DateTimeOffset EndDatetime { get; set; }
        public string Location { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Code { get; set; }

        public CreateClassroomModel(int id, int trainerUserId, int trainingId, string name, List<int> traineeUsersIdList, DateTimeOffset beginDatetime, DateTimeOffset endDatetime, string location, int createdBy, DateTimeOffset createdAt, string code)
        {
            Id = id;
            TrainerUserId = trainerUserId;
            TrainingId = trainingId;
            Name = name;
            TraineeUsersIdList = traineeUsersIdList;
            BeginDatetime = beginDatetime;
            EndDatetime = endDatetime;
            Location = location;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            Code = code;
        }
    }
}
