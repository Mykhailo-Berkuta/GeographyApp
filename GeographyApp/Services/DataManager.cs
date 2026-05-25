using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using GeographyApp.Models;
using Region = GeographyApp.Models.Region;

namespace GeographyApp.Services
{
    public class DataManager
    {
        // Шлях до файлу збереження даних
        private readonly string _filePath = "geography_data.json";

        public List<Continent> Continents { get; private set; } = new();

        public List<Country> Countries { get; private set; } = new();

        public List<Region> Regions { get; private set; } = new();

        public List<City> Cities { get; private set; } = new();

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
            var kharkiv = new City("Харків", 1_433_000, 49.9935, 36.2304,
                kharkivRegion, ukraine);
            var kyiv = new City("Київ", 2_962_000, 50.4501, 30.5234,
                kyivRegion, ukraine);
            Cities.AddRange(new[] { kharkiv, kyiv });
        }
    }
}