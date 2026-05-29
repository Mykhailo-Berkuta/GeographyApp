using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using GeographyApp.Models;
using Region = GeographyApp.Models.Region;

namespace GeographyApp.Services
{
    /// <summary>
    /// Клас для зберігання та керування колекціями географічних даних (материки, країни, регіони, міста).
    /// Забезпечує завантаження, збереження та ініціалізацію тестових даних.
    /// </summary>
    public class DataManager
    {
        // Шлях до файлу збереження даних
        private readonly string _filePath = "geography_data.json";

        /// <summary>
        /// Колекція материків, що зберігається в пам'яті.
        /// </summary>
        public List<Continent> Continents { get; private set; } = new();

        /// <summary>
        /// Колекція країн, що зберігається в пам'яті.
        /// </summary>
        public List<Country> Countries { get; private set; } = new();

        /// <summary>
        /// Колекція регіонів, що зберігається в пам'яті.
        /// </summary>
        public List<Region> Regions { get; private set; } = new();

        /// <summary>
        /// Колекція міст, що зберігається в пам'яті.
        /// </summary>
        public List<City> Cities { get; private set; } = new();

        /// <summary>
        /// Завантажує зразкові дані у колекції (використовується як запасний варіант).
        /// </summary>
        public void LoadSampleData()
        {
            // Материки
            var europe = new Continent("Європа", 746_000_000, 10_530_000);
            var asia = new Continent("Азія", 4_700_000_000, 44_579_000);
            Continents.AddRange(new[] { europe, asia });

            // Країни
            var ukraine = new Country("Україна", 43_528_000, 603_550,
                "Республіка", "Київ", europe);
            var germany = new Country("Німеччина", 83_200_000, 357_114,
                "Федеральна республіка", "Берлін", europe);
            Countries.AddRange(new[] { ukraine, germany });

            // Регіони
            var kharkivRegion = new Region("Харківська область", 2_600_000,
                "Область", "Харків", ukraine);
            var kyivRegion = new Region("Київська область", 1_800_000,
                "Область", "Київ", ukraine);
            Regions.AddRange(new[] { kharkivRegion, kyivRegion });

            // Міста
            var kharkiv = new City("Харків", 1_433_000, kharkivRegion, ukraine, 50.0039, 36.2304);
            var kyiv = new City("Київ", 2_962_000, kyivRegion, ukraine, 50.4501, 30.5234);
            Cities.AddRange(new[] { kharkiv, kyiv });
        }

        /// <summary>
        /// Зберігає поточні дані у JSON-файл.
        /// </summary>
        public void Save()
        {
            var data = new AppData
            {
                Continents = Continents,
                Countries = Countries.Select(c => new CountryDto
                {
                    Name = c.Name,
                    Population = c.Population,
                    Area = c.Area,
                    GovernmentForm = c.GovernmentForm,
                    Capital = c.Capital,
                    ContinentName = c.Continent.Name
                }).ToList(),
                Regions = Regions.Select(r => new RegionDto
                {
                    Name = r.Name,
                    Population = r.Population,
                    RegionType = r.RegionType,
                    Capital = r.Capital,
                    CountryName = r.Country.Name
                }).ToList(),
                Cities = Cities.Select(c => new CityDto
                {
                    Name = c.Name,
                    Population = c.Population,
                    RegionName = c.Region.Name,
                    CountryName = c.Country.Name,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude
                }).ToList()
            };

            var json = JsonSerializer.Serialize(data,
    new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });

            File.WriteAllText(_filePath, json);
        }

        /// <summary>
        /// Завантажує дані з JSON-файлу. Якщо файл не існує — завантажує тестові дані.
        /// При завантаженні ігнорує записи, чиї батьківські об'єкти не знайдено.
        /// </summary>
        public void Load()
        {
            if (!File.Exists(_filePath))
            {
                LoadSampleData();
                return;
            }

            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<AppData>(json);
            if (data == null) return;

            Continents = data.Continents;

            Countries = data.Countries.Select(d =>
            {
                var continent = Continents.FirstOrDefault(c => c.Name == d.ContinentName);
                if (continent == null) return null;
                return new Country(d.Name, d.Population, d.Area, d.GovernmentForm, d.Capital, continent);
            }).Where(c => c != null).Select(c => c!).ToList();

            Regions = data.Regions.Select(d =>
            {
                var country = Countries.FirstOrDefault(c => c.Name == d.CountryName);
                if (country == null) return null;
                return new Region(d.Name, d.Population, d.RegionType, d.Capital, country);
            }).Where(r => r != null).Select(r => r!).ToList();

            Cities = data.Cities.Select(d =>
            {
                var region = Regions.FirstOrDefault(r => r.Name == d.RegionName);
                var country = Countries.FirstOrDefault(c => c.Name == d.CountryName);
                if (region == null || country == null) return null;
                return new City(d.Name, d.Population, region, country, d.Latitude, d.Longitude);
            }).Where(c => c != null).Select(c => c!).ToList();
        }

        // DTO класи для серіалізації
        private class AppData
        {
            public List<Continent> Continents { get; set; } = new();
            public List<CountryDto> Countries { get; set; } = new();
            public List<RegionDto> Regions { get; set; } = new();
            public List<CityDto> Cities { get; set; } = new();
        }

        private class CountryDto
        {
            public string Name { get; set; }
            public long Population { get; set; }
            public double Area { get; set; }
            public string GovernmentForm { get; set; }
            public string Capital { get; set; }
            public string ContinentName { get; set; }
        }

        private class RegionDto
        {
            public string Name { get; set; }
            public long Population { get; set; }
            public string RegionType { get; set; }
            public string Capital { get; set; }
            public string CountryName { get; set; }
        }

        private class CityDto
        {
            public string Name { get; set; }
            public long Population { get; set; }
            public string RegionName { get; set; }
            public string CountryName { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}