namespace API.Models.DTO
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public CompanyContactDto CompanyContact { get; set; }
    }
}