using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public struct TrainersListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ProfessionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Trainer, TrainersListModel>> Projection => trainer => new TrainersListModel
        {
            Id = trainer.Id,
            DepartmentId = trainer.DepartmentId,
            Name = trainer.Name,
            PhoneNumber = trainer.PhoneNumber,
            Surname = trainer.Surname,
            CreatedBy = trainer.CreatedBy,
            ProfessionId = trainer.ProfessionId,
            Email = trainer.Email,
            UpdatedBy = trainer.UpdatedBy,
            UpdatedAt = trainer.UpdatedAt,
            CreatedAt = trainer.CreatedAt
        };

        public static TrainersListModel Create(Trainer trainer)
        {
            return Projection.Compile().Invoke(trainer);
        }
    }
}
