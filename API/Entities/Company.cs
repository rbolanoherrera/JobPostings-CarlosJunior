using System.Collections.Generic;

namespace API.Entities
{
    public partial class Company
    {
        public Company()
        {
            JobCompanies = new HashSet<JobCompanies>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual CompanyContact CompanyContact { get; set; }
        public virtual ICollection<JobCompanies> JobCompanies { get; set; }
    }
}
