using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

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
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return _sortBy switch
            {
                SortBy.Name => CompareNames(x.Name, y.Name),
                // For population we want larger values first (descending)
                SortBy.Population => y.Population.CompareTo(x.Population),
                _ => 0
            };
        }

        private static int CompareNames(string a, string b)
        {
            // Normalize strings to remove accents and use current culture comparison
            var normalizedA = a.Normalize(NormalizationForm.FormKD);
            var normalizedB = b.Normalize(NormalizationForm.FormKD);
            return string.Compare(normalizedA, normalizedB, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        }
    }
}