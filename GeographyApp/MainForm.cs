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
            _dataManager.Load();
            ShowContinents(); // показуємо материки при запуску
        }

        // Навігаційні кнопки 

        private void btnContinents_Click(object sender, EventArgs e) => ShowContinents();
        private void btnCountries_Click(object sender, EventArgs e) => ShowCountries();
        private void btnRegions_Click(object sender, EventArgs e) => ShowRegions();
        private void btnCities_Click(object sender, EventArgs e) => ShowCities();

        private void ShowContinents()
        {
            dataGridView.Tag = "continents";
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
            dataGridView.Tag = "countries";
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
            dataGridView.Tag = "regions";
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
            dataGridView.Tag = "cities";
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
            if (dataGridView.Tag?.ToString() == "continents")
            {
                using var form = new Forms.ContinentForm();
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Continents.Add(form.Result);
                    ShowContinents();
                }
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                if (_dataManager.Continents.Count == 0)
                {
                    MessageBox.Show("Спочатку додайте хоча б один материк!",
                        "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using var form = new Forms.CountryForm(_dataManager.Continents);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Countries.Add(form.Result);
                    ShowCountries();
                }
            }

            else if (dataGridView.Tag?.ToString() == "regions")
            {
                if (_dataManager.Countries.Count == 0)
                {
                    MessageBox.Show("Спочатку додайте хоча б одну країну!",
                        "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using var form = new Forms.RegionForm(_dataManager.Countries);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Regions.Add(form.Result);
                    ShowRegions();
                }
            }

            else if (dataGridView.Tag?.ToString() == "cities")
            {
                if (_dataManager.Countries.Count == 0)
                {
                    MessageBox.Show("Спочатку додайте хоча б одну країну!",
                        "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using var form = new Forms.CityForm(
                    _dataManager.Countries, _dataManager.Regions);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Cities.Add(form.Result);
                    ShowCities();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            int index = dataGridView.CurrentRow.Index;

            if (dataGridView.Tag?.ToString() == "continents")
            {
                var continent = _dataManager.Continents[index];
                using var form = new Forms.ContinentForm(continent);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Continents[index] = form.Result;
                    ShowContinents();
                }
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                var country = _dataManager.Countries[index];
                using var form = new Forms.CountryForm(_dataManager.Continents, country);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Countries[index] = form.Result;
                    ShowCountries();
                }
            }
            else if (dataGridView.Tag?.ToString() == "regions")
            {
                var region = _dataManager.Regions[index];
                using var form = new Forms.RegionForm(_dataManager.Countries, region);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Regions[index] = form.Result;
                    ShowRegions();
                }
            }
            else if (dataGridView.Tag?.ToString() == "cities")
            {
                var city = _dataManager.Cities[index];
                using var form = new Forms.CityForm(
                    _dataManager.Countries, _dataManager.Regions, city);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _dataManager.Cities[index] = form.Result;
                    ShowCities();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            int index = dataGridView.CurrentRow.Index;

            var confirm = MessageBox.Show(
                "Ви впевнені що хочете видалити цей запис?",
                "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            if (dataGridView.Tag?.ToString() == "continents")
            {
                _dataManager.Continents.RemoveAt(index);
                ShowContinents();
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                _dataManager.Countries.RemoveAt(index);
                ShowCountries();
            }
            else if (dataGridView.Tag?.ToString() == "regions")
            {
                _dataManager.Regions.RemoveAt(index);
                ShowRegions();
            }
            else if (dataGridView.Tag?.ToString() == "cities")
            {
                _dataManager.Cities.RemoveAt(index);
                ShowCities();
            }
        }

        //збереження при закритті форми
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _dataManager.Save();
        }
    }
}