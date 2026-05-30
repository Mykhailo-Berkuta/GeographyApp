using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GeographyApp.Models.Comparers
{
    /// <summary>
    /// Порівнювач для Country — підтримує сортування за назвою, населенням і площею.
    /// </summary>
    public class CountryComparer : IComparer<Country>
    {
        public enum SortBy { Name, Population, Area }

        private readonly SortBy _sortBy;

        /// <summary>
        /// Ініціалізує порівнювач для країн з обраним режимом сортування.
        /// </summary>
        /// <param name="sortBy">Режим сортування (назва, населення, площа).</param>
        public CountryComparer(SortBy sortBy)
        {
            _sortBy = sortBy;
        }

        /// <summary>
        /// Порівнює дві країни згідно вибраного режиму.
        /// </summary>
        /// <param name="x">Перша країна.</param>
        /// <param name="y">Друга країна.</param>
        /// <returns>
        /// Відповідь менше 0, якщо x перед y в сортуванні;
        /// більше 0, якщо x після y;
        /// 0, якщо вони рівні.
        /// </returns>
        public int Compare(Country? x, Country? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return _sortBy switch
            {
                SortBy.Name => string.Compare(Normalize(x.Name), Normalize(y.Name), CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace),
                // Population: larger first
                SortBy.Population => y.Population.CompareTo(x.Population),
                // Area: larger first
                SortBy.Area => y.Area.CompareTo(x.Area),
                _ => 0
            };
        }

        private static string Normalize(string s) => s.Normalize(NormalizationForm.FormKD);
    }
}