using System.Collections.Generic;

namespace API.Entities
{
    public partial class Job
    {
        public Job()
        {
            JobCompanies = new HashSet<JobCompanies>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public long PublisherId { get; set; }

        public virtual JobCategory Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<JobCompanies> JobCompanies { get; set; }
    }
}