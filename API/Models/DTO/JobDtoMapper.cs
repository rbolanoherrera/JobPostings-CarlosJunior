using API.Entities;

namespace API.Models.DTO
{
    public static class JobDtoMapper
    {
        public static JobDto MapToDto(Job job)
        {
            return new JobDto()
            {
                Id = job.Id,
                Title = job.Title,

                Category = new CategoryDto()
                {
                    Name = job.Category.Name
                },
                Publisher = new PublisherDto()
                {
                    Name = job.Publisher.Name
                }
            };
        }
    }
}
