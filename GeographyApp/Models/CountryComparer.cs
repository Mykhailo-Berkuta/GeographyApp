using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    public class CountryComparer : IComparer<Country>
    {
        public enum SortBy { Name, Population, Area }

        private readonly SortBy _sortBy;

        public CountryComparer(SortBy sortBy)
        {
            _sortBy = sortBy;
        }

        public int Compare(Country? x, Country? y)
        {
            if (x == null || y == null) return 0;

            return _sortBy switch
            {
                SortBy.Name => string.Compare(x.Name, y.Name,
                    StringComparison.OrdinalIgnoreCase),
                SortBy.Population => x.Population.CompareTo(y.Population),
                SortBy.Area => x.Area.CompareTo(y.Area),
                _ => 0
            };
        }
    }
}