using System.Threading.Tasks;
using System.Web.Mvc;
using Weather.Services;

namespace Weather.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;

        public WeatherController()
        {
            weatherService = new WeatherService(new OpenWeatherQueryBuilder());
        }

        // GET: Weather
        public ActionResult Index()
        {
            ViewData["cities"] = weatherService.DefaultCities;

            return View(ViewData["cities"]);
        }

        public async Task<ActionResult> Detail(string city, int cnt = 1)
        {
            if (city == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["cities"] = weatherService.DefaultCities;

            var info = await weatherService.GetByCityNameAsync(city, cnt);

            return View(info);
        }
    }
}