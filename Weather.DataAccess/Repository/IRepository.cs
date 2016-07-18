using System;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.DataAccess.Repository
{
    public interface IRepository<in TKey, TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Query { get; }

        Task<TEntity> GetAsync(TKey key);

        TEntity AddNew(Action<TEntity> update);

        Task DeleteAsync(TKey key);

        Task<int> SaveChangesAsync();
    }
}