using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class City : GeographicObject
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Region Region { get; set; }

        public Country Country { get; set; }

        public City(string name, long population, double latitude,
                    double longitude, Region region, Country country)
            : base(name, population)
        {
            Latitude = latitude;
            Longitude = longitude;
            Region = region;
            Country = country;
        }
    }
}