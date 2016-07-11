using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Weather.Enums;
using Weather.Extensions;

namespace Weather.Services
{
    public class OpenWeatherQueryBuilder : IOpenWeatherQueryBuilder, IDailyQueryBuilder
    {
        private readonly string path;
        private readonly IDictionary<string, string> queryParams = new Dictionary<string, string>();

        private static string ApiKey => ConfigurationManager.AppSettings["WeatherApiKey"];
        private static string RootUrl => ConfigurationManager.AppSettings["WeatherRootUrl"];

        public OpenWeatherQueryBuilder()
        {
        }

        private OpenWeatherQueryBuilder(string path)
        {
            this.path = path;
        }

        public IDailyQueryBuilder Daily()
        {
            return new OpenWeatherQueryBuilder("daily");
        }

        public IDailyQueryBuilder City(string city)
        {
            queryParams.Add("q", city);

            return this;
        }

        public IDailyQueryBuilder Units(UnitsEnum units)
        {
            queryParams.Add("units", units.ToString());

            return this;
        }

        public IDailyQueryBuilder Lang(LanguageEnum lang)
        {
            queryParams.Add("lang", lang.ToString());

            return this;
        }

        public IDailyQueryBuilder ForDays(int amount)
        {
            queryParams.Add("cnt", amount.ToString());

            return this;
        }

        public HttpRequestMessage Build()
        {
            queryParams.Add("APPID", ApiKey);

            var uriBuilder = new UriBuilder($"{RootUrl}/{path}")
            {
                Query = queryParams.ToQueryString()
            };

            return new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
        }
    }
}