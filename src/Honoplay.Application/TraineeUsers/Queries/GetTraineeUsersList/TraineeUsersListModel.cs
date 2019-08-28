using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList
{
    public struct TraineeUsersListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }

        public static Expression<Func<TraineeUser, TraineeUsersListModel>> Projection => traineeUser => new TraineeUsersListModel
        {
            Id = traineeUser.Id,
            Name = traineeUser.Name,
            Surname = traineeUser.Surname,
            NationalIdentityNumber = traineeUser.NationalIdentityNumber,
            PhoneNumber = traineeUser.PhoneNumber,
            Gender = traineeUser.Gender,
            UpdatedBy = traineeUser.UpdatedBy,
            UpdatedAt = traineeUser.UpdatedAt,
            CreatedAt = traineeUser.CreatedAt,
            CreatedBy = traineeUser.CreatedBy,
            WorkingStatusId = traineeUser.WorkingStatusId,
            DepartmentId = traineeUser.DepartmentId
        };

        public static TraineeUsersListModel Create(TraineeUser traineeUser)
        {
            return Projection.Compile().Invoke(traineeUser);
        }
    }
}
