using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Weather.DataAccess.Repository;
using Weather.Models.City;

namespace Weather.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository cityRepository;

        public CityController()
        {
            cityRepository = new CityRepository();
        }

        public async Task<ActionResult> Index()
        {
            return View(await cityRepository.Query
                                            .Select(c => new CityViewModel
                                            {
                                                Id = c.Id,
                                                Name = c.Name,
                                                UaName = c.UaName,
                                                ViewCount = c.Detail.ViewCount,
                                                LastVisited = c.Detail.LastVisitedDate
                                            })
                                            .ToArrayAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UaName,Name")] CityCreateModel city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            Session.Remove("DefaultCities");

            cityRepository.AddNew(newCity =>
            {
                newCity.Name = city.Name;
                newCity.UaName = city.UaName;
            });

            await cityRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = await cityRepository.Query
                                           .Where(c => c.Id == id)
                                           .Select(c => new CityEditModel
                                           {
                                               Id = c.Id,
                                               Name = c.Name,
                                               UaName = c.UaName
                                           })
                                           .SingleOrDefaultAsync();

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UaName,Name")] CityEditModel editedCity)
        {
            if (!ModelState.IsValid)
            {
                return View(editedCity);
            }

            var city = await cityRepository.GetAsync(editedCity.Id);

            if (city == null)
            {
                return HttpNotFound();
            }

            Session.Remove("DefaultCities");

            city.Name = editedCity.Name;
            city.UaName = editedCity.UaName;

            await cityRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = await cityRepository.Query
                                           .Where(c => c.Id == id)
                                           .Select(c => new CityViewModel
                                           {
                                               Id = c.Id,
                                               Name = c.Name,
                                               UaName = c.UaName,
                                               ViewCount = c.Detail.ViewCount
                                           })
                                           .SingleOrDefaultAsync();

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var city = await cityRepository.GetAsync(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            Session.Remove("DefaultCities");

            await cityRepository.DeleteAsync(city.Id);

            await cityRepository.SaveChangesAsync();

            return RedirectToAction("Index");
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
