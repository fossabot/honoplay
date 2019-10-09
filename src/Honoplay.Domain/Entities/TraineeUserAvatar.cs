namespace Honoplay.Domain.Entities
{
    public class TraineeUserAvatar
    {
        public int AvatarId { get; set; }
        public int TraineeUserId { get; set; }

        public Avatar Avatar { get; set; }
        public TraineeUser TraineeUser { get; set; }
    }
}
