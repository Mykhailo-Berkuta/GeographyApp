using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class City : GeographicObject
    {
        public Region Region { get; set; }

        public Country Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public City(string name, long population, Region region, Country country, double latitude = 0, double longitude = 0)
            : base(name, population)
        {
            Region = region;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}