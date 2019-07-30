using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Level : BaseEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
        public Classroom Classroom { get; set; }
    }
}
