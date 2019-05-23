using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public struct TrainerDetailModel
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

        private static Expression<Func<Trainer, TrainerDetailModel>> Projection
        {
            get
            {
                return trainer => new TrainerDetailModel
                {
                    Id = trainer.Id,
                    UpdatedBy = trainer.UpdatedBy,
                    UpdatedAt = trainer.UpdatedAt,
                    CreatedBy = trainer.CreatedBy,
                    CreatedAt = trainer.CreatedAt,
                    Name = trainer.Name,
                    Surname = trainer.Surname,
                    PhoneNumber = trainer.PhoneNumber,
                    DepartmentId = trainer.DepartmentId,
                    ProfessionId = trainer.ProfessionId,
                    Email = trainer.Email
                };
            }
        }

        public static TrainerDetailModel Create(Trainer trainer)
        {
            return Projection.Compile().Invoke(trainer);
        }
    }
}
