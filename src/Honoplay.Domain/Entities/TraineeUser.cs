using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class TraineeUser : BaseEntity
    {
        public TraineeUser()
        {
            ClassroomTraineeUsers = new HashSet<ClassroomTraineeUser>();
            TraineeUsersAvatars = new HashSet<TraineeUserAvatar>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }
        public int? TraineeGroupId { get; set; }
        public Guid? ProfilPhotoId { get; set; }

        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset LastPasswordChangeDateTime { get; set; }
        public int NumberOfInvalidPasswordAttemps { get; set; }


        public virtual ICollection<ClassroomTraineeUser> ClassroomTraineeUsers { get; set; }
        public virtual ICollection<TraineeUserAvatar> TraineeUsersAvatars { get; set; }

        [JsonIgnore]
        public WorkingStatus WorkingStatus { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public TraineeGroup TraineeGroup { get; set; }
        [JsonIgnore]
        public ContentFile ContentFile { get; set; }
    }
}
