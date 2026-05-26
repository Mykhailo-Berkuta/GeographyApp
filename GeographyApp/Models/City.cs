using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class City : GeographicObject
    {
        public Region Region { get; set; }

        public Country Country { get; set; }

        public City(string name, long population, Region region, Country country)
            : base(name, population)
        {
            Region = region;
            Country = country;
        }
    }
}