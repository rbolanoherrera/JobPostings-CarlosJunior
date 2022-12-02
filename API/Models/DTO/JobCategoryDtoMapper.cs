using API.Entities;

namespace API.Models.DTO
{
    public static class JobCategoryDtoMapper
    {
        public static JobCategoryDto MapToDto(JobCategory jobCategory)
        {
            return new JobCategoryDto()
            {
                Id = jobCategory.Id,
                Name = jobCategory.Name
            };
        }
    }
}