using System;
using System.Collections.Generic;
using System.Data.Entity;
using Weather.DataAccess.Entity;

namespace Weather.DataAccess.Context
{
    public class WeatherContextInitializer : DropCreateDatabaseIfModelChanges<WeatherDb>
    {
        protected override void Seed(WeatherDb context)
        {
            var cities = new List<City>
            {
                CreateCity("Lviv", "Львів"),
                CreateCity("Odessa", "Одеса"),
                CreateCity("Kyiv", "Київ"),
                CreateCity("Kharkiv", "Харків"),
                CreateCity("Dnipropetrovsk", "Дніпропетровськ")
            };

            cities.ForEach(c => context.Cities.Add(c));

            context.SaveChanges();
        }

        private static City CreateCity(string name, string uaName)
        {
            return new City
            {
                Name = name,
                UaName = uaName,
                DateCreated = DateTime.UtcNow,
                Detail = new CityDetail()
            };
        }
    }
}