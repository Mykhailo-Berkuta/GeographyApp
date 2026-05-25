using GeographyApp.Models;
using GeographyApp.Services;

namespace GeographyApp
{
    public partial class MainForm : Form
    {
        // зберігає всі колекції
        private readonly DataManager _dataManager = new();

        public MainForm()
        {
            InitializeComponent();
            _dataManager.LoadSampleData();
            ShowContinents(); // показуємо материки при запуску
        }

        // Навігаційні кнопки 

        private void btnContinents_Click(object sender, EventArgs e) => ShowContinents();
        private void btnCountries_Click(object sender, EventArgs e) => ShowCountries();
        private void btnRegions_Click(object sender, EventArgs e) => ShowRegions();
        private void btnCities_Click(object sender, EventArgs e) => ShowCities();

        private void ShowContinents()
        {
            dataGridView.DataSource = _dataManager.Continents
                .Select(c => new
                {
                    Назва = c.Name,
                    Населення = c.Population.ToString("N0"),
                    Площа_км2 = c.Area.ToString("N0")
                }).ToList();
            statusLabel.Text = $"Материки: {_dataManager.Continents.Count} записів";
        }

        private void ShowCountries()
        {
            dataGridView.Tag = "continents";
            dataGridView.DataSource = _dataManager.Countries
                .Select(c => new
                {
                    Назва = c.Name,
                    Материк = c.Continent.Name,
                    Населення = c.Population.ToString("N0"),
                    Площа_км2 = c.Area.ToString("N0"),
                    Столиця = c.Capital,
                    Форма_правління = c.GovernmentForm
                }).ToList();
            statusLabel.Text = $"Країни: {_dataManager.Countries.Count} записів";
        }

        private void ShowRegions()
        {
            dataGridView.DataSource = _dataManager.Regions
                .Select(r => new
                {
                    Назва = r.Name,
                    Тип = r.RegionType,
                    Країна = r.Country.Name,
                    Населення = r.Population.ToString("N0"),
                    Адм_центр = r.Capital
                }).ToList();
            statusLabel.Text = $"Регіони: {_dataManager.Regions.Count} записів";
        }

        private void ShowCities()
        {
            dataGridView.DataSource = _dataManager.Cities
                .Select(c => new
                {
                    Назва = c.Name,
                    Країна = c.Country.Name,
                    Регіон = c.Region.Name,
                    Населення = c.Population.ToString("N0"),
                    Широта = c.Latitude,
                    Довгота = c.Longitude
                }).ToList();
            statusLabel.Text = $"Міста: {_dataManager.Cities.Count} записів";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Визначаємо який розділ зараз активний
            // і відкриваємо відповідну форму
            if (dataGridView.Tag?.ToString() == "continents")
            {
                using var form = new Forms.ContinentForm();
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Continents.Add(form.Result);
                    ShowContinents();
                }
            }
        }
    }
}