using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Weather.DataAccess.Repository;
using Weather.Models.City;
using Weather.Services;

namespace Weather.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;
        private readonly ICityRepository cityRepository;

        public WeatherController()
        {
            cityRepository = new CityRepository();
            weatherService = new WeatherService(new OpenWeatherQueryBuilder());
        }

        // GET: Weather
        public async Task<ActionResult> Index()
        {
            ViewBag.DefaultCities = (await cityRepository.GetDefaultCitiesAsync())
                                                         .Select(c => new DefaultCityModel
                                                         {
                                                             Name = c.Name,
                                                             UaName = c.UaName
                                                         });

            Session.Add("DefaultCities", ViewBag.DefaultCities);

            return View();
        }

        public async Task<ActionResult> Detail(string city, int cnt = 1)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }
            
            ViewBag.DefaultCities = Session["DefaultCities"] ?? (await cityRepository.GetDefaultCitiesAsync())
                                                                                     .Select(c => new DefaultCityModel
                                                                                     {
                                                                                         Name = c.Name,
                                                                                         UaName = c.UaName
                                                                                     });

            var info = await weatherService.GetByCityNameAsync(city, cnt);

            if (info != null)
            {
                var name = info.City.Name;

                await cityRepository.UpdateViewStatisticsAsync(name);
                await cityRepository.SaveChangesAsync();
            }

            return View(info);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cityRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}