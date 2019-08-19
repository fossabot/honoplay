using System;
using System.Collections.Generic;

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public struct CreateClassroomModel
    {
        public int Id { get; }
        public int TrainerId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public List<int> TraineesIdList { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateClassroomModel(int id, int trainerId, int trainingId, string name, List<int> traineesIdList, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            TrainerId = trainerId;
            TrainingId = trainingId;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            TraineesIdList = traineesIdList;
        }
    }
}
