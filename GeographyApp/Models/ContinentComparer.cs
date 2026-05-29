using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GeographyApp.Models
{
    /// <summary>
    /// Порівнювач для Continent — підтримує сортування за назвою, населенням і площею.
    /// Імена порівнюються з урахуванням культури.
    /// </summary>
    public class ContinentComparer : IComparer<Continent>
    {
        public enum SortBy { Name, Population, Area }

        private readonly SortBy _sortBy;

        /// <summary>
        /// Створює екземпляр порівнювача для континентів.
        /// </summary>
        /// <param name="sortBy">Критерій сортування: за назвою, населенням або площею.</param>
        public ContinentComparer(SortBy sortBy)
        {
            _sortBy = sortBy;
        }

        /// <summary>
        /// Порівнює два континенти згідно вибраного режиму.
        /// </summary>
        /// <param name="x">Перший континент для порівняння.</param>
        /// <param name="y">Другий континент для порівняння.</param>
        /// <returns>
        /// Менше ніж нуль, якщо <paramref name="x"/> менше ніж <paramref name="y"/>;
        /// більше ніж нуль, якщо <paramref name="x"/> більше ніж <paramref name="y"/>;
        /// нуль, якщо вони рівні.
        /// </returns>
        public int Compare(Continent? x, Continent? y)
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