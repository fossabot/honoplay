using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class ContentFile : BaseEntity
    {
        public ContentFile()
        {
            TraineeUsers = new HashSet<TraineeUser>();
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        public virtual ICollection<TraineeUser> TraineeUsers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
