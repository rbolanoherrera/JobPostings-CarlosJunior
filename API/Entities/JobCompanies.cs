namespace API.Entities
{
    public partial class JobCompanies
    {
        public long JobId { get; set; }
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Job Job { get; set; }
    }
}