using System;

namespace Weather.DataAccess.Entity
{
    public class City
    {
        public int Id { get; set; }

        public string UaName { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual CityDetail Detail { get; set; }
    }
}