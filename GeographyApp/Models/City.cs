using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    /// <summary>
    /// Представляє місто з посиланнями на регіон та країну, а також координатами (широта/довгота).
    /// </summary>
    public class City : GeographicObject
    {
        /// <summary>
        /// Регіон, до якого належить місто.
        /// </summary>
        public Region Region { get; set; }

        /// <summary>
        /// Країна, до якої належить місто.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Широта міста.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Довгота міста.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Створює новий екземпляр City з вказаними значеннями.
        /// </summary>
        /// <param name="name">Назва міста.</param>
        /// <param name="population">Населення міста.</param>
        /// <param name="region">Регіон, до якого належить місто.</param>
        /// <param name="country">Країна, до якої належить місто.</param>
        /// <param name="latitude">Широта (за замовчуванням 0).</param>
        /// <param name="longitude">Довгота (за замовчуванням 0).</param>
        public City(string name, long population, Region region, Country country, double latitude = 0, double longitude = 0)
            : base(name, population)
        {
            Region = region;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}