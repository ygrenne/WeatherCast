using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    public interface IWeatherService
    {
        Task<WeatherDaily.RootObject> GetByCityNameAsync(string city, int cnt = 1);
    }
}