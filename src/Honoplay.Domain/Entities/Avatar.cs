using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Avatar
    {
        public Avatar()
        {
            TraineeUsers = new HashSet<TraineeUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBytes { get; set; }

        public virtual ICollection<TraineeUser> TraineeUsers { get; set; }
    }
}
