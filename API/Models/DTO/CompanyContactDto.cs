namespace API.Models.DTO
{
    public class CompanyContactDto
    {
        public CompanyContactDto()
        {

        }
        public long CompanyId { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }
    }
}