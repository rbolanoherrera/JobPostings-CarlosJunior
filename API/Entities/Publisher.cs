using System.Collections.Generic;

namespace API.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            Jobs = new HashSet<Job>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}