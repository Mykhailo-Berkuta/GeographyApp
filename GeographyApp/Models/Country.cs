using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class Country : GeographicObject
    {
        public double Area { get; set; }

        public string GovernmentForm { get; set; }

        public string Capital { get; set; }

        public Continent Continent { get; set; }

        public Country(string name, long population, double area,
                       string governmentForm, string capital, Continent continent)
            : base(name, population)
        {
            Area = area;
            GovernmentForm = governmentForm;
            Capital = capital;
            Continent = continent;
        }
    }
}