using System.Collections.Generic;

namespace API.Models.DTO
{
    public class JobDto
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public CategoryDto Category { get; set; }
        public PublisherDto Publisher { get; set; }
        public ICollection<CompanyDto> Companies { get; set; }
    }
}