using System;

namespace Weather.Models.City
{
    public class CityViewModel : BaseCityModel
    {
        public int Id { get; set; }

        public int ViewCount { get; set; }

        public DateTime? LastVisited { get; set; }
    }
}