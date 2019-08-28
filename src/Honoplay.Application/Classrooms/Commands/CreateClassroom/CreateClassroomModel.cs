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
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateClassroomModel(int id, int trainerUserId, int trainingId, string name, List<int> traineeUsersIdList, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            TrainerUserId = trainerUserId;
            TrainingId = trainingId;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            TraineeUsersIdList = traineeUsersIdList;
        }
    }
}
