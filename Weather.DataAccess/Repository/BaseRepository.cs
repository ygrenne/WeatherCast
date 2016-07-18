using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.DataAccess.Repository
{
    public abstract class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        protected DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

        public IQueryable<TEntity> Query => DbSet;

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await DbSet.FindAsync(key);
        }

        public abstract TEntity AddNew(Action<TEntity> update);

        public async Task DeleteAsync(TKey key)
        {
            var entity = await GetAsync(key);

            DbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        protected BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
            }
            catch (ObjectDisposedException)
            {
                //ignore
            }
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                dbContext.Dispose();
            }
        }
    }
}