using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class GeographicObjectComparer : IComparer<GeographicObject>
    {
        public enum SortBy { Name, Population }

        private readonly SortBy _sortBy;

        public GeographicObjectComparer(SortBy sortBy)
        {
            _sortBy = sortBy;
        }

        public int Compare(GeographicObject? x, GeographicObject? y)
        {
            if (x == null || y == null) return 0;

            return _sortBy switch
            {
                SortBy.Name => string.Compare(x.Name, y.Name,
                    StringComparison.OrdinalIgnoreCase),
                SortBy.Population => x.Population.CompareTo(y.Population),
                _ => 0
            };
        }
    }
}