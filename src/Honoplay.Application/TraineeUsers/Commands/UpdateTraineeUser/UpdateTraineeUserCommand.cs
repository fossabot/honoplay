using System;
using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser
{
    public class UpdateTraineeUserCommand : IRequest<ResponseModel<UpdateTraineeUserModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int? AvatarId { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }

    }
}
