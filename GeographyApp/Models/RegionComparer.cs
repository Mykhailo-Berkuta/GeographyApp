using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class RegionComparer : IComparer<Region>
    {
        public enum SortBy { Name, Population, Type }

        private readonly SortBy _sortBy;

        public RegionComparer(SortBy sortBy)
        {
            _sortBy = sortBy;
        }

        public int Compare(Region? x, Region? y)
        {
            if (x == null || y == null) return 0;

            return _sortBy switch
            {
                SortBy.Name => string.Compare(x.Name, y.Name,
                    StringComparison.OrdinalIgnoreCase),
                SortBy.Population => x.Population.CompareTo(y.Population),
                SortBy.Type => string.Compare(x.RegionType, y.RegionType,
                    StringComparison.OrdinalIgnoreCase),
                _ => 0
            };
        }
    }
}