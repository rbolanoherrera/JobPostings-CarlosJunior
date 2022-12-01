using System.Collections.Generic;

namespace API.Entities
{
    public class JobCategory
    {
        public JobCategory()
        {
            Job = new HashSet<Job>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
