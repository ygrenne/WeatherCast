using System.Threading.Tasks;
using Weather.DataAccess.Entity;

namespace Weather.DataAccess.Repository
{
    public interface ICityRepository : IRepository<int, City>
    {
        Task UpdateViewStatisticsAsync(string name);

        Task<City[]> GetDefaultCitiesAsync(int amount = 5); 
    }
}