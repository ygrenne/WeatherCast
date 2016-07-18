using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Weather.DataAccess.Context;
using Weather.DataAccess.Entity;

namespace Weather.DataAccess.Repository
{
    public class CityRepository : BaseRepository<int, City>, ICityRepository
    {
        public CityRepository() : base(new WeatherDb())
        {
        }

        public CityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override City AddNew(Action<City> update)
        {
            var newCity = new City
            {
                DateCreated = DateTime.UtcNow,
                Detail = new CityDetail()
            };

            update(newCity);

            DbSet.Add(newCity);

            return newCity;
        }

        public async Task UpdateViewStatisticsAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var details = await Query.Where(c => c.Name == name)
                                     .Select(c => c.Detail)
                                     .SingleOrDefaultAsync();

                if (details != null)
                {
                    details.ViewCount++;
                    details.LastVisitedDate = DateTime.UtcNow;
                }
            } 
        } 

        public async Task<City[]> GetDefaultCitiesAsync(int amount = 5)
        {
            return await Query.OrderByDescending(c => c.DateCreated)
                              .Take(() => amount)
                              .ToArrayAsync();
        }
    }
}