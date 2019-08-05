using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Game
    {
        public Game()
        {
            Sessions = new HashSet<Session>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
