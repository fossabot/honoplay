using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId
{
    public struct ClassroomsListByTrainingIdModel
    {
        public int Id { get; private set; }
        public int TrainerUserId { get; private set; }
        public int TrainingId { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<Classroom, ClassroomsListByTrainingIdModel>> Projection
        {
            get
            {
                return classroom => new ClassroomsListByTrainingIdModel
                {
                    Id = classroom.Id,
                    Name = classroom.Name,
                    TrainerUserId = classroom.TrainerUserId,
                    TrainingId = classroom.TrainingId,
                    CreatedBy = classroom.CreatedBy,
                    CreatedAt = classroom.CreatedAt,
                    UpdatedBy = classroom.UpdatedBy,
                    UpdatedAt = classroom.UpdatedAt,
                };
            }
        }
        public static ClassroomsListByTrainingIdModel Create(Classroom classroom)
        {
            return Projection.Compile().Invoke(classroom);
        }
    }
}
