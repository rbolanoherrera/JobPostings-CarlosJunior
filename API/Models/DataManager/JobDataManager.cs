using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.DataManager
{
    public class JobDataManager : IDataRepository<Job, JobDto>
    {
        readonly JobPostingsContext _jobStoreContext;

        public JobDataManager(JobPostingsContext storeContext)
        {
            _jobStoreContext = storeContext;
        }

        public IEnumerable<Job> GetAll()
        {
            return _jobStoreContext.Job
                .Include(job => job.JobCompanies)
                .ToList();
        }

        public Job Get(long id)
        {
            _jobStoreContext.ChangeTracker.LazyLoadingEnabled = false;

            var job = _jobStoreContext.Job
                .SingleOrDefault(b => b.Id == id);

            if (job == null)
            {
                return null;
            }

            _jobStoreContext.Entry(job)
                .Collection(b => b.JobCompanies)
                .Load();

            _jobStoreContext.Entry(job)
                .Reference(b => b.Category)
                .Load();

            _jobStoreContext.Entry(job)
                .Reference(b => b.Publisher)
                .Load();

            return job;
        }

        public JobDto GetDto(long id)
        {
            _jobStoreContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new JobPostingsContext())
            {
                var job = context.Job
                    .SingleOrDefault(b => b.Id == id);

                return JobDtoMapper.MapToDto(job);
            }
        }

        public void Add(Job entity)
        {
            _jobStoreContext.Job.Add(entity);
            _jobStoreContext.SaveChanges();
        }

        public void Update(Job entityToUpdate, Job entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Job job)
        {
            _jobStoreContext.Entry(job).State = EntityState.Deleted;
            _jobStoreContext.SaveChanges();
        }
    }
}