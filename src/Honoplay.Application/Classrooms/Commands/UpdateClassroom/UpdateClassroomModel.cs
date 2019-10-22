using System;
using System.Collections.Generic;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public struct UpdateClassroomModel
    {
        public int Id { get; }
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }
        public List<int> TraineeUsersIdList { get; set; }
        public DateTimeOffset BeginDatetime { get; set; }
        public DateTimeOffset EndDatetime { get; set; }
        public string Location { get; set; }

        public UpdateClassroomModel(int id, int trainerUserId, int trainingId, string name, int updatedBy, DateTimeOffset updatedAt, List<int> traineeUsersIdList, DateTimeOffset beginDatetime, DateTimeOffset endDatetime, string location)
        {
            Id = id;
            TrainerUserId = trainerUserId;
            TrainingId = trainingId;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            TraineeUsersIdList = traineeUsersIdList;
            BeginDatetime = beginDatetime;
            EndDatetime = endDatetime;
            Location = location;
        }
    }
}
