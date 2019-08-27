using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUserDetail
{
    public struct TrainerUserDetailModel
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

        private static Expression<Func<TrainerUser, TrainerUserDetailModel>> Projection
        {
            get
            {
                return trainerUser => new TrainerUserDetailModel
                {
                    Id = trainerUser.Id,
                    UpdatedBy = trainerUser.UpdatedBy,
                    UpdatedAt = trainerUser.UpdatedAt,
                    CreatedBy = trainerUser.CreatedBy,
                    CreatedAt = trainerUser.CreatedAt,
                    Name = trainerUser.Name,
                    Surname = trainerUser.Surname,
                    PhoneNumber = trainerUser.PhoneNumber,
                    DepartmentId = trainerUser.DepartmentId,
                    ProfessionId = trainerUser.ProfessionId,
                    Email = trainerUser.Email
                };
            }
        }

        public static TrainerUserDetailModel Create(TrainerUser trainerUser)
        {
            return Projection.Compile().Invoke(trainerUser);
        }
    }
}
