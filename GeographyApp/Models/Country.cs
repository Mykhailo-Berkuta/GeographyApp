using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    /// <summary>
    /// Представляє країну з додатковими полями: площа, форма правління, столиця та посилання на материк.
    /// </summary>
    public class Country : GeographicObject
    {
        /// <summary>
        /// Площа країни в квадратних кілометрах.
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Форма правління країни.
        /// </summary>
        public string GovernmentForm { get; set; }

        /// <summary>
        /// Столиця країни.
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// Посилання на материк, до якого належить країна.
        /// </summary>
        public Continent Continent { get; set; }

        /// <summary>
        /// Створює новий екземпляр Country з вказаними значеннями.
        /// </summary>
        /// <param name="name">Назва країни.</param>
        /// <param name="population">Населення країни.</param>
        /// <param name="area">Площа країни в км².</param>
        /// <param name="governmentForm">Форма правління.</param>
        /// <param name="capital">Столиця.</param>
        /// <param name="continent">Материк, до якого належить країна.</param>
        public Country(string name, long population, double area,
                       string governmentForm, string capital, Continent continent)
            : base(name, population)
        {
            Area = area;
            GovernmentForm = governmentForm;
            Capital = capital;
            Continent = continent;
        }
    }
}