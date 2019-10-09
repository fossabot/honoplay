using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersListByClassroomId
{
    public struct GetTraineeUsersListByClassroomIdModel
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int? AvatarId { get; set; }
        public int DepartmentId { get; set; }


        public static Expression<Func<TraineeUser, GetTraineeUsersListByClassroomIdModel>> Projection => traineeUser => new GetTraineeUsersListByClassroomIdModel
        {
            Id = traineeUser.Id,
            Email = traineeUser.Email,
            CreatedAt = traineeUser.CreatedAt,
            CreatedBy = traineeUser.CreatedBy,
            UpdatedBy = traineeUser.UpdatedBy,
            UpdatedAt = traineeUser.UpdatedAt,
            Gender = traineeUser.Gender,
            PhoneNumber = traineeUser.PhoneNumber,
            NationalIdentityNumber = traineeUser.NationalIdentityNumber,
            Surname = traineeUser.Surname,
            Name = traineeUser.Name,
            WorkingStatusId = traineeUser.WorkingStatusId,
            AvatarId = traineeUser.AvatarId,
            DepartmentId = traineeUser.DepartmentId
        };
        public static GetTraineeUsersListByClassroomIdModel Create(TraineeUser traineeUser) => Projection.Compile().Invoke(traineeUser);
    }
}
