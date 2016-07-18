using System.Data.Entity;

namespace Weather.DataAccess.Context
{
    public class WeatherDbConfig : DbConfiguration
    {
        public WeatherDbConfig()
        {
            SetDatabaseInitializer(new WeatherContextInitializer());
        }
    }
}