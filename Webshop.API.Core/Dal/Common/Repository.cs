using Microsoft.EntityFrameworkCore;

namespace Webshop.API.Core.Dal.Common
{
    public abstract class Repository <T> : IRepository<T> where T : EntityBase
    {
        private ApiContext _dbContext;

        protected Repository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T? GetById(int identifier) => _dbContext.Set<T>().Find(identifier);
        public IQueryable<T> List() => _dbContext.Set<T>().AsQueryable();
        public void Delete(T value) => _dbContext.Set<T>().Remove(value);
        public void Add(T value) => _dbContext.Set<T>().Add(value);
        public void Edit(T value) => _dbContext.Entry(value).State = EntityState.Modified;

        public async Task SaveAsync(IEnumerable<T> entitiesToCleanTracking)
        {
            _dbContext.SaveChanges();
            foreach(var entity in entitiesToCleanTracking)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task SaveAsync(T entitiesToCleanTracking)
        {
            _dbContext.SaveChanges();
            _dbContext.Entry(entitiesToCleanTracking).State = EntityState.Deleted;
        }
    }
}
