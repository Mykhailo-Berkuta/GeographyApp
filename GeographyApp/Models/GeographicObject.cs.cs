using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public abstract class GeographicObject
    {
        public string Name { get; set; }

        public long Population { get; set; }

        protected GeographicObject(string name, long population)
        {
            Name = name;
            Population = population;
        }

        public override string ToString() => Name;
    }
}