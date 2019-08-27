using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList
{
    public struct TrainerUsersListModel
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

        public static Expression<Func<TrainerUser, TrainerUsersListModel>> Projection => trainerUser => new TrainerUsersListModel
        {
            Id = trainerUser.Id,
            DepartmentId = trainerUser.DepartmentId,
            Name = trainerUser.Name,
            PhoneNumber = trainerUser.PhoneNumber,
            Surname = trainerUser.Surname,
            CreatedBy = trainerUser.CreatedBy,
            ProfessionId = trainerUser.ProfessionId,
            Email = trainerUser.Email,
            UpdatedBy = trainerUser.UpdatedBy,
            UpdatedAt = trainerUser.UpdatedAt,
            CreatedAt = trainerUser.CreatedAt
        };

        public static TrainerUsersListModel Create(TrainerUser trainerUser)
        {
            return Projection.Compile().Invoke(trainerUser);
        }
    }
}
