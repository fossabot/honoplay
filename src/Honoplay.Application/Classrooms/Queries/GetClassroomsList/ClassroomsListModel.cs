using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsList
{
    public struct ClassroomsListModel
    {
        public int Id { get; private set; }
        public int TrainerUserId { get; private set; }
        public int TrainingId { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset BeginDatetime { get; set; }
        public DateTimeOffset EndDatetime { get; set; }
        public string Location { get; set; }
        public string Code { get; set; }

        public static Expression<Func<Classroom, ClassroomsListModel>> Projection
        {
            get
            {
                return classroom => new ClassroomsListModel
                {
                    Id = classroom.Id,
                    Name = classroom.Name,
                    TrainerUserId = classroom.TrainerUserId,
                    TrainingId = classroom.TrainingId,
                    CreatedBy = classroom.CreatedBy,
                    CreatedAt = classroom.CreatedAt,
                    UpdatedBy = classroom.UpdatedBy,
                    UpdatedAt = classroom.UpdatedAt,
                    BeginDatetime = classroom.BeginDatetime,
                    EndDatetime = classroom.EndDatetime,
                    Location = classroom.Location,
                    Code = classroom.Code
                };
            }
        }
        public static ClassroomsListModel Create(Classroom classroom)
        {
            return Projection.Compile().Invoke(classroom);
        }
    }
}
