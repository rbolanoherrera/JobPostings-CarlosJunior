using API.Entities;

namespace API.Models.DTO
{
    public static class CompanyDtoMapper
    {
        public static CompanyDto MapToDto(Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                Name = company.Name,

                CompanyContact = new CompanyContactDto()
                {
                    CompanyId = company.Id,
                    Address = company.CompanyContact?.Address,
                    ContactNumber = company.CompanyContact?.ContactNumber
                }
            };
        }
    }
}
