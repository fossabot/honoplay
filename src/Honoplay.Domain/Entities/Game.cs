using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Level> Levels { get; set; }
    }
}
