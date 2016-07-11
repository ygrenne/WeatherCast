using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weather.Models
{
    public class WeatherDaily
    {
        public class Coord
        {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string UaName { get; set; }
            public Coord Coord { get; set; }
            public string Country { get; set; }
            public int Population { get; set; }
        }

        public class Temp
        {
            public double Day { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public double Night { get; set; }
            public double Eve { get; set; }
            public double Morn { get; set; }
        }

        public class Weather
        {
            public int Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class List
        {
            private readonly DateTime offsetDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            public double Dt { get; set; }
            public Temp Temp { get; set; }
            public double Pressure { get; set; }
            public int Humidity { get; set; }
            public List<Weather> Weather { get; set; }
            public double Speed { get; set; }
            public int Deg { get; set; }
            public int Clouds { get; set; }
            public double Rain { get; set; }

            public DateTime Date => offsetDate.AddSeconds(Dt);
        }

        public class RootObject
        {
            public City City { get; set; }
            public string Cod { get; set; }
            public double Message { get; set; }
            public int Cnt { get; set; }
            public List<List> List { get; set; }
        }
    }
}