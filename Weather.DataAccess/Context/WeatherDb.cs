using System.Data.Entity;
using System.Reflection;
using Weather.DataAccess.Entity;

namespace Weather.DataAccess.Context
{
    [DbConfigurationType(typeof(WeatherDbConfig))]
    public class WeatherDb : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public WeatherDb() : base("WeatherDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(City)));

            base.OnModelCreating(modelBuilder);
        }
    }
}