using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Avatar
    {
        public Avatar()
        {
            TraineeUsersAvatars = new HashSet<TraineeUserAvatar>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBytes { get; set; }

        public virtual ICollection<TraineeUserAvatar> TraineeUsersAvatars { get; set; }
    }
}
