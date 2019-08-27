using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser
{
    /// <summary>
    /// /api/TraineeUser/Post service need that model for create traineeUser.
    /// </summary>
    public class CreateTraineeUserCommand : IRequest<ResponseModel<CreateTraineeUserModel>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
}
