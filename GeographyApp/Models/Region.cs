using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class Region : GeographicObject
    {
        public string RegionType { get; set; }

        public string Capital { get; set; }

        public Country Country { get; set; }

        public Region(string name, long population, string regionType,
                      string capital, Country country)
            : base(name, population)
        {
            RegionType = regionType;
            Capital = capital;
            Country = country;
        }
    }
}