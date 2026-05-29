using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    /// <summary>
    /// Представляє адміністративний регіон всередині країни (наприклад область або штат).
    /// Містить тип регіону, адміністративний центр та посилання на країну.
    /// </summary>
    public class Region : GeographicObject
    {
        /// <summary>
        /// Тип регіону (наприклад: область, штат).
        /// </summary>
        public string RegionType { get; set; }

        /// <summary>
        /// Адміністративний центр регіону.
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// Посилання на країну, до якої належить регіон.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Створює новий екземпляр Region з вказаними значеннями.
        /// </summary>
        /// <param name="name">Назва регіону.</param>
        /// <param name="population">Населення регіону.</param>
        /// <param name="regionType">Тип регіону.</param>
        /// <param name="capital">Адміністративний центр.</param>
        /// <param name="country">Країна, до якої належить регіон.</param>
        public Region(string name, long population, string regionType,
                      string capital, Country country)
            : base(name, population)
        {
            RegionType = regionType;
            Capital = capital;
            Country = country;
        }
    }
}