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