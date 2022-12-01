using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.DataManager
{
    public class PublisherDataManager : IDataRepository<Publisher, PublisherDto>
    {
        readonly JobPostingsContext _jobStoreContext;

        public PublisherDataManager(JobPostingsContext storeContext)
        {
            _jobStoreContext = storeContext;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _jobStoreContext.Publisher.ToList();
        }

        public Publisher Get(long id)
        {
            var publisher = _jobStoreContext.Publisher
                .SingleOrDefault(b => b.Id == id);

            return publisher;
        }

        public PublisherDto GetDto(long id)
        {
            _jobStoreContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new JobPostingsContext())
            {
                var publisher = context.Publisher
                    .SingleOrDefault(b => b.Id == id);

                return PublisherDtoMapper.MapToDto(publisher);
            }
        }


        public void Add(Publisher entity)
        {
            _jobStoreContext.Publisher.Add(entity);
            _jobStoreContext.SaveChanges();
        }

        public void Update(Publisher entityToUpdate, Publisher entity)
        {
            entityToUpdate = _jobStoreContext.Publisher
                .Single(b => b.Id == entityToUpdate.Id);

            entityToUpdate.Name = entity.Name;

            _jobStoreContext.SaveChanges();
        }

        public void Delete(Publisher entity)
        {
            _jobStoreContext.Entry(entity).State = EntityState.Deleted;
            _jobStoreContext.SaveChanges();
        }
    }
}
