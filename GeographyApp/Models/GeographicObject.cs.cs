using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public abstract class GeographicObject : IComparable<GeographicObject>
    {
        public string Name { get; set; }

        public long Population { get; set; }

        protected GeographicObject(string name, long population)
        {
            Name = name;
            Population = population;
        }

        public override string ToString() => Name;

        public int CompareTo(GeographicObject? other)
        {
            if (other == null) return 1;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}