using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class Continent : GeographicObject
    {
        public double Area { get; set; }

        public Continent(string name, long population, double area)
            : base(name, population)
        {
            Area = area;
        }
    }
}