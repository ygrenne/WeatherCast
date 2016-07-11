using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Weather.Models;
using System.Threading.Tasks;
using Weather.Enums;

namespace Weather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherQueryBuilder queryBuilder;

        IReadOnlyList<WeatherDaily.City> IWeatherService.DefaultCities => new[]
        {
            new WeatherDaily.City {UaName = "Львів", Name = "Lviv"},
            new WeatherDaily.City {UaName = "Київ", Name = "Kyiv"},
            new WeatherDaily.City {UaName = "Харків", Name = "Kharkiv"},
            new WeatherDaily.City {UaName = "Дніпропетровськ", Name = "Dnipropetrovsk"},
            new WeatherDaily.City {UaName = "Одеса", Name = "Odessa"}
        };

        public WeatherService(IOpenWeatherQueryBuilder queryBuilder)
        {
            this.queryBuilder = queryBuilder;
        }

        public async Task<WeatherDaily.RootObject> GetByCityNameAsync(string city,int cnt = 1)
        {
            HttpRequestMessage request = queryBuilder.Daily()
                                                     .City(city)
                                                     .Units(UnitsEnum.Metric)
                                                     .Lang(LanguageEnum.Ua)
                                                     .ForDays(cnt)
                                                     .Build();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(request.RequestUri);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WeatherDaily.RootObject>(content);
            }
        }
    }
}