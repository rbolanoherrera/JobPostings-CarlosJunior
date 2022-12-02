using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.DataManager
{
    public class JobCategoryDataManager : IDataRepository<JobCategory, JobCategoryDto>
    {
        readonly JobPostingsContext _jobStoreContext;

        public JobCategoryDataManager(JobPostingsContext storeContext)
        {
            _jobStoreContext = storeContext;
        }

        public IEnumerable<JobCategory> GetAll()
        {
            return _jobStoreContext.JobCategory.ToList();
        }

        public JobCategory Get(long id)
        {
            var jobc = _jobStoreContext.JobCategory
                .SingleOrDefault(b => b.Id == id);

            return jobc;
        }

        public JobCategoryDto GetDto(long id)
        {
            _jobStoreContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new JobPostingsContext())
            {
                var jobc = context.JobCategory
                    .SingleOrDefault(b => b.Id == id);

                return JobCategoryDtoMapper.MapToDto(jobc);
            }
        }

        public void Add(JobCategory entity)
        {
            _jobStoreContext.JobCategory.Add(entity);
            _jobStoreContext.SaveChanges();
        }

        public void Update(JobCategory entityToUpdate, JobCategory entity)
        {
            entityToUpdate = _jobStoreContext.JobCategory
                .Single(b => b.Id == entityToUpdate.Id);

            entityToUpdate.Name = entity.Name;

            _jobStoreContext.SaveChanges();
        }

        public void Delete(JobCategory entity)
        {
            _jobStoreContext.Entry(entity).State = EntityState.Deleted;
            _jobStoreContext.SaveChanges();
        }

    }
}