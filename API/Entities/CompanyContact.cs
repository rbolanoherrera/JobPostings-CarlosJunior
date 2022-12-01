namespace API.Entities
{
    public class CompanyContact
    {
        public long CompanyId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public virtual Company Company { get; set; }
    }
}