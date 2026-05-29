using System;
using System.Collections.Generic;
using System.Text;

namespace GeographyApp.Models
{
    /// <summary>
    /// Базовий абстрактний клас для географічних об'єктів, що мають назву та населення.
    /// Забезпечує порівняння за назвою.
    /// </summary>
    public abstract class GeographicObject : IComparable<GeographicObject>
    {
        /// <summary>
        /// Назва об'єкта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Населення об'єкта.
        /// </summary>
        public long Population { get; set; }

        /// <summary>
        /// Конструктор для ініціалізації базових полів географічного об'єкта.
        /// </summary>
        /// <param name="name">Назва об'єкта.</param>
        /// <param name="population">Населення об'єкта.</param>
        protected GeographicObject(string name, long population)
        {
            Name = name;
            Population = population;
        }

        /// <summary>
        /// Повертає рядкове представлення об'єкта (назва).
        /// </summary>
        public override string ToString() => Name;

        /// <summary>
        /// Порівнює два географічні об'єкти за назвою (без урахування регістру).
        /// </summary>
        public int CompareTo(GeographicObject? other)
        {
            if (other == null) return 1;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}