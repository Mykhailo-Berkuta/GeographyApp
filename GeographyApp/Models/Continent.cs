using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    /// <summary>
    /// Представляє материк з назвою, населенням та площею.
    /// </summary>
    public class Continent : GeographicObject
    {
        /// <summary>
        /// Площа материка в квадратних кілометрах.
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Створює новий екземпляр Continent з вказаними значеннями.
        /// </summary>
        /// <param name="name">Назва материка.</param>
        /// <param name="population">Населення материка.</param>
        /// <param name="area">Площа материка в км².</param>
        public Continent(string name, long population, double area)
            : base(name, population)
        {
            Area = area;
        }
    }
}