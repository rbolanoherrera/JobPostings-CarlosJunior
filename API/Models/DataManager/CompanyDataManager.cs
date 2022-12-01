using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.DataManager
{
    public class CompanyDataManager : IDataRepository<Company, CompanyDto>
    {
        readonly JobPostingsContext _jobStoreContext;

        public CompanyDataManager(JobPostingsContext storeContext)
        {
            _jobStoreContext = storeContext;
        }

        public IEnumerable<Company> GetAll()
        {
            return _jobStoreContext.Company
                .Include(company => company.CompanyContact)
                .ToList();
        }

        public Company Get(long id)
        {
            var company = _jobStoreContext.Company
                .SingleOrDefault(b => b.Id == id);

            return company;
        }

        public CompanyDto GetDto(long id)
        {
            _jobStoreContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new JobPostingsContext())
            {
                var company = context.Company
                    .SingleOrDefault(b => b.Id == id);

                return CompanyDtoMapper.MapToDto(company);
            }
        }


        public void Add(Company entity)
        {
            _jobStoreContext.Company.Add(entity);
            _jobStoreContext.SaveChanges();
        }

        public void Update(Company entityToUpdate, Company entity)
        {
            entityToUpdate = _jobStoreContext.Company
                .Include(a => a.JobCompanies)
                .Include(a => a.CompanyContact)
                .Single(b => b.Id == entityToUpdate.Id);

            entityToUpdate.Name = entity.Name;

            entityToUpdate.CompanyContact.Address = entity.CompanyContact.Address;
            entityToUpdate.CompanyContact.ContactNumber = entity.CompanyContact.ContactNumber;

            var deletedBooks = entityToUpdate.JobCompanies.Except(entity.JobCompanies).ToList();
            var addedBooks = entity.JobCompanies.Except(entityToUpdate.JobCompanies).ToList();

            deletedBooks.ForEach(bookToDelete =>
                entityToUpdate.JobCompanies.Remove(
                    entityToUpdate.JobCompanies
                        .First(b => b.JobId == bookToDelete.JobId)));

            foreach (var addedBook in addedBooks)
            {
                _jobStoreContext.Entry(addedBook).State = EntityState.Added;
            }

            _jobStoreContext.SaveChanges();
        }

        public void Delete(Company company)
        {
            _jobStoreContext.Entry(company).State = EntityState.Deleted;
            _jobStoreContext.SaveChanges();
        }
    }
}
