using API.Entities;

namespace API.Models.DTO
{
    public static class PublisherDtoMapper
    {
        public static PublisherDto MapToDto(Publisher company)
        {
            return new PublisherDto()
            {
                Id = company.Id,
                Name = company.Name
            };
        }
    }
}
