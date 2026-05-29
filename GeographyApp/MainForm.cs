using GeographyApp.Models;
using GeographyApp.Services;

namespace GeographyApp
{
    public partial class MainForm : Form
    {
        // зберігає всі колекції
        private readonly DataManager _dataManager = new();
        
        // Для фільтрації по батьківському об'єкту
        private string? _continentFilter;
        private string? _countryFilter;
        private string? _regionFilter;

        public MainForm()
        {
            InitializeComponent();
            _dataManager.Load();
            ShowContinents();
            KeyDown += MainForm_KeyDown;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            txtSearch.TextBox.PlaceholderText = "Пошук...";

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 210);
            btnSort.DropDownItemClicked += BtnSort_DropDownItemClicked;
            
            // Додаємо обробник подвійного клику для фільтрації по батьківському об'єкту
            dataGridView.DoubleClick += DataGridView_DoubleClick;
        }

        // Навігаційні кнопки 

        private void btnContinents_Click(object sender, EventArgs e)
        {
            _continentFilter = null;
            _countryFilter = null;
            _regionFilter = null;
            txtSearch.Text = string.Empty;
            ShowContinents();
        }

        private void btnCountries_Click(object sender, EventArgs e)
        {
            _countryFilter = null;
            _regionFilter = null;
            txtSearch.Text = string.Empty;
            ShowCountries();
        }

        private void btnRegions_Click(object sender, EventArgs e)
        {
            _regionFilter = null;
            txtSearch.Text = string.Empty;
            ShowRegions();
        }

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
            
            var countries = _dataManager.Countries;
            if (!string.IsNullOrEmpty(_continentFilter))
            {
                countries = countries.Where(c => c.Continent.Name == _continentFilter).ToList();
            }
            
            dataGridView.DataSource = countries
                .Select(c => new
                {
                    Назва = c.Name,
                    Материк = c.Continent.Name,
                    Населення = c.Population.ToString("N0"),
                    Площа_км2 = c.Area.ToString("N0"),
                    Столиця = c.Capital,
                    Форма_правління = c.GovernmentForm
                }).ToList();
            
            string filterInfo = !string.IsNullOrEmpty(_continentFilter) ? $" (Матерік: {_continentFilter})" : "";
            statusLabel.Text = $"Країни: {countries.Count} записів{filterInfo}";
        }

        private void ShowRegions()
        {
            dataGridView.Tag = "regions";
            
            var regions = _dataManager.Regions;
            if (!string.IsNullOrEmpty(_countryFilter))
            {
                regions = regions.Where(r => r.Country.Name == _countryFilter).ToList();
            }
            
            dataGridView.DataSource = regions
                .Select(r => new
                {
                    Назва = r.Name,
                    Тип = r.RegionType,
                    Країна = r.Country.Name,
                    Населення = r.Population.ToString("N0"),
                    Адм_центр = r.Capital
                }).ToList();
            
            string filterInfo = !string.IsNullOrEmpty(_countryFilter) ? $" (Країна: {_countryFilter})" : "";
            statusLabel.Text = $"Регіони: {regions.Count} записів{filterInfo}";
        }

        private void ShowCities()
        {
            dataGridView.Tag = "cities";
            
            var cities = _dataManager.Cities;
            if (!string.IsNullOrEmpty(_regionFilter))
            {
                cities = cities.Where(c => c.Region.Name == _regionFilter).ToList();
            }
            
            dataGridView.DataSource = cities
                .Select(c => new
                {
                    Назва = c.Name,
                    Країна = c.Country.Name,
                    Регіон = c.Region.Name,
                    Населення = c.Population.ToString("N0"),
                    Широта = c.Latitude.ToString("F4"),
                    Довгота = c.Longitude.ToString("F4")
                }).ToList();
            
            string filterInfo = !string.IsNullOrEmpty(_regionFilter) ? $" (Регіон: {_regionFilter})" : "";
            statusLabel.Text = $"Міста: {cities.Count} записів{filterInfo}";
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
                var continent = _dataManager.Continents[index];
                var dependentCountries = _dataManager.Countries
                    .Count(c => c.Continent.Name == continent.Name);

                if (dependentCountries > 0)
                {
                    MessageBox.Show(
                        $"Неможливо видалити материк. Існує {dependentCountries} країн(и), " +
                        "що належать цьому материку. Спочатку видаліть їх.",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _dataManager.Continents.RemoveAt(index);
                ShowContinents();
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                var country = _dataManager.Countries[index];
                var dependentRegions = _dataManager.Regions
                    .Count(r => r.Country.Name == country.Name);
                var dependentCities = _dataManager.Cities
                    .Count(c => c.Country.Name == country.Name);

                if (dependentRegions > 0 || dependentCities > 0)
                {
                    MessageBox.Show(
                        $"Неможливо видалити країну. Існує {dependentRegions} регіон(ів) та " +
                        $"{dependentCities} місто(міст), що належать цій країні. " +
                        "Спочатку видаліть їх.",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _dataManager.Countries.RemoveAt(index);
                ShowCountries();
            }
            else if (dataGridView.Tag?.ToString() == "regions")
            {
                var region = _dataManager.Regions[index];
                var dependentCities = _dataManager.Cities
                    .Count(c => c.Region.Name == region.Name);

                if (dependentCities > 0)
                {
                    MessageBox.Show(
                        $"Неможливо видалити регіон. Існує {dependentCities} місто(міст), " +
                        "що належать цьому регіону. Спочатку видаліть їх.",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                // Якщо пошук порожній показуємо всі дані
                RefreshCurrentView();
                return;
            }

            if (dataGridView.Tag?.ToString() == "continents")
            {
                dataGridView.DataSource = _dataManager.Continents
                    .Where(c => c.Name.ToLower().Contains(query))
                    .Select(c => new
                    {
                        Назва = c.Name,
                        Населення = c.Population.ToString("N0"),
                        Площа_км2 = c.Area.ToString("N0")
                    }).ToList();
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                dataGridView.DataSource = _dataManager.Countries
                    .Where(c => c.Name.ToLower().Contains(query) ||
                                c.Capital.ToLower().Contains(query) ||
                                c.Continent.Name.ToLower().Contains(query))
                    .Select(c => new
                    {
                        Назва = c.Name,
                        Материк = c.Continent.Name,
                        Населення = c.Population.ToString("N0"),
                        Площа_км2 = c.Area.ToString("N0"),
                        Столиця = c.Capital,
                        Форма_правління = c.GovernmentForm
                    }).ToList();
            }
            else if (dataGridView.Tag?.ToString() == "regions")
            {
                dataGridView.DataSource = _dataManager.Regions
                    .Where(r => r.Name.ToLower().Contains(query) ||
                                r.Country.Name.ToLower().Contains(query))
                    .Select(r => new
                    {
                        Назва = r.Name,
                        Тип = r.RegionType,
                        Країна = r.Country.Name,
                        Населення = r.Population.ToString("N0"),
                        Адм_центр = r.Capital
                    }).ToList();
            }
            else if (dataGridView.Tag?.ToString() == "cities")
            {
                dataGridView.DataSource = _dataManager.Cities
                    .Where(c => c.Name.ToLower().Contains(query) ||
                                c.Country.Name.ToLower().Contains(query) ||
                                c.Region.Name.ToLower().Contains(query))
                    .Select(c => new
                    {
                        Назва = c.Name,
                        Країна = c.Country.Name,
                        Регіон = c.Region.Name,
                        Населення = c.Population.ToString("N0"),
                        Широта = c.Latitude.ToString("F4"),
                        Довгота = c.Longitude.ToString("F4")
                    }).ToList();
            }

            statusLabel.Text = $"Знайдено записів: {dataGridView.Rows.Count}";
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void RefreshCurrentView()
        {
            switch (dataGridView.Tag?.ToString())
            {
                case "continents": ShowContinents(); break;
                case "countries": ShowCountries(); break;
                case "regions": ShowRegions(); break;
                case "cities": ShowCities(); break;
            }
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Виділіть запис у таблиці.",
                    "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dataGridView.CurrentRow.Index;
            string query = "";

            if (dataGridView.Tag?.ToString() == "continents")
            {
                var continent = _dataManager.Continents[index];
                query = continent.Name;
            }
            else if (dataGridView.Tag?.ToString() == "countries")
            {
                var country = _dataManager.Countries[index];
                query = country.Name;
            }
            else if (dataGridView.Tag?.ToString() == "regions")
            {
                var region = _dataManager.Regions[index];
                query = $"{region.Name}, {region.Country.Name}";
            }
            else if (dataGridView.Tag?.ToString() == "cities")
            {
                var city = _dataManager.Cities[index];
                query = $"{city.Name}, {city.Country.Name}";
            }

            string url = $"https://www.google.com/maps/search/{Uri.EscapeDataString(query)}";
            using var mapForm = new Forms.MapForm(url, query);
            mapForm.ShowDialog(this);
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            _dataManager.Save();
            MessageBox.Show("Дані збережено!", "Збереження",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuLoad_Click(object sender, EventArgs e)
        {
            _dataManager.Load();
            RefreshCurrentView();
            MessageBox.Show("Дані завантажено!", "Завантаження",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void пошукToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Встановлюємо фокус на поле пошуку
            txtSearch.Focus();
            txtSearch.SelectAll();
        }

        private void menuStats_Click(object sender, EventArgs e)
        {
            var stats = _dataManager.Continents
                .Select(c => new
                {
                    Материк = c.Name,
                    Населення = c.Population.ToString("N0"),
                    Країн = _dataManager.Countries
                        .Count(co => co.Continent.Name == c.Name),
                    Міст = _dataManager.Cities
                        .Count(ci => ci.Country.Continent.Name == c.Name)
                }).ToList();

            dataGridView.DataSource = stats;
            statusLabel.Text = "Статистика населення по материках";
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Географічний довідник\n\n" +
                "Програма для зберігання та перегляду\n" +
                "географічних об'єктів: материків, країн,\n" +
                "регіонів та міст.\n\n" +
                "Автор: Беркута М.С.\n" +
                "ХНУРЕ, 2026",
                "Про програму",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(
                    "Географічний довідник - довідка\n\n" +
                    "Навігація:\n" +
                    "Кнопки ліворуч - перемикання між розділами\n\n" +
                    "Дії з записами:\n" +
                    "Додати - додати новий запис\n" +
                    "Редагувати - змінити виділений запис\n" +
                    "Видалити - видалити виділений запис\n" +
                    "На карті - відкрити у Google Maps\n\n" +
                    "Пошук - введіть текст і натисніть кнопку пошуку\n\n" +
                    "Файл → Зберегти / Завантажити - робота з файлом\n" +
                    "Карта → Статистика населення - зведена таблиця\n\n" +
                    "F1 - ця довідка",
                    "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void btnSortByName_Click(object sender, EventArgs e)
        {
            var comparer = new GeographicObjectComparer(GeographicObjectComparer.SortBy.Name);
            SortCurrentView(comparer, CountryComparer.SortBy.Name);
        }

        private void btnSortByPopulation_Click(object sender, EventArgs e)
        {
            var comparer = new GeographicObjectComparer(GeographicObjectComparer.SortBy.Population);
            SortCurrentView(comparer, CountryComparer.SortBy.Population, ContinentComparer.SortBy.Population);
        }

        private void btnSortByArea_Click(object sender, EventArgs e)
        {
            var tag = dataGridView.Tag?.ToString();

            if (tag == "continents")
            {
                _dataManager.Continents.Sort(new ContinentComparer(ContinentComparer.SortBy.Area));
                ShowContinents();
            }
            else if (tag == "countries")
            {
                _dataManager.Countries.Sort(new CountryComparer(CountryComparer.SortBy.Area));
                ShowCountries();
            }
            else
            {
                MessageBox.Show("Сортування за площею доступне лише для материків і країн.",
                    "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SortCurrentView(GeographicObjectComparer comparer,
            CountryComparer.SortBy countrySortBy,
            ContinentComparer.SortBy continentSortBy = ContinentComparer.SortBy.Name)
        {
            string tag = dataGridView.Tag?.ToString();
            string query = txtSearch.Text.Trim().ToLower();

            switch (tag)
            {
                case "continents":
                    {
                        var list = _dataManager.Continents;
                        var filtered = string.IsNullOrEmpty(query)
                            ? list.ToList()
                            : list.Where(c => c.Name.ToLower().Contains(query)).ToList();

                        filtered.Sort(new ContinentComparer(continentSortBy));

                        dataGridView.DataSource = filtered
                            .Select(c => new
                            {
                                Назва = c.Name,
                                Населення = c.Population.ToString("N0"),
                                Площа_км2 = c.Area.ToString("N0")
                            }).ToList();

                        statusLabel.Text = $"Материки: {filtered.Count} записів";
                    }
                    break;
                case "countries":
                    {
                        var list = _dataManager.Countries;
                        var filtered = string.IsNullOrEmpty(query)
                            ? list.ToList()
                            : list.Where(c => c.Name.ToLower().Contains(query) ||
                                              c.Capital.ToLower().Contains(query) ||
                                              c.Continent.Name.ToLower().Contains(query)).ToList();

                        filtered.Sort(new CountryComparer(countrySortBy));

                        dataGridView.DataSource = filtered
                            .Select(c => new
                            {
                                Назва = c.Name,
                                Материк = c.Continent.Name,
                                Населення = c.Population.ToString("N0"),
                                Площа_км2 = c.Area.ToString("N0"),
                                Столиця = c.Capital,
                                Форма_правління = c.GovernmentForm
                            }).ToList();

                        statusLabel.Text = $"Країни: {filtered.Count} записів";
                    }
                    break;
                case "regions":
                    {
                        var list = _dataManager.Regions;
                        var filtered = string.IsNullOrEmpty(query)
                            ? list.ToList()
                            : list.Where(r => r.Name.ToLower().Contains(query) ||
                                              r.Country.Name.ToLower().Contains(query)).ToList();

                        filtered.Sort(comparer);

                        dataGridView.DataSource = filtered
                            .Select(r => new
                            {
                                Назва = r.Name,
                                Тип = r.RegionType,
                                Країна = r.Country.Name,
                                Населення = r.Population.ToString("N0"),
                                Адм_центр = r.Capital
                            }).ToList();

                        statusLabel.Text = $"Регіони: {filtered.Count} записів";
                    }
                    break;
                case "cities":
                    {
                        var list = _dataManager.Cities;
                        var filtered = string.IsNullOrEmpty(query)
                            ? list.ToList()
                            : list.Where(c => c.Name.ToLower().Contains(query) ||
                                              c.Country.Name.ToLower().Contains(query) ||
                                              c.Region.Name.ToLower().Contains(query)).ToList();

                        filtered.Sort(comparer);

                        dataGridView.DataSource = filtered
                            .Select(c => new
                            {
                                Назва = c.Name,
                                Країна = c.Country.Name,
                                Регіон = c.Region.Name,
                                Населення = c.Population.ToString("N0"),
                                Широта = c.Latitude.ToString("F4"),
                                Довгота = c.Longitude.ToString("F4")
                            }).ToList();

                        statusLabel.Text = $"Міста: {filtered.Count} записів";
                    }
                    break;
            }
        }

        private void BtnSort_DropDownItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                var text = e.ClickedItem?.Text ?? string.Empty;
                if (text == "За назвою")
                {
                    btnSortByName_Click(sender, EventArgs.Empty);
                }
                else if (text == "За населенням")
                {
                    btnSortByPopulation_Click(sender, EventArgs.Empty);
                }
                else if (text == "За площею")
                {
                    btnSortByArea_Click(sender, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при сортуванні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_DoubleClick(object? sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;

            string tag = dataGridView.Tag?.ToString() ?? string.Empty;

            if (tag == "continents")
            {
                // Отримуємо назву матеріку прямо з DataGridView
                var continentName = dataGridView.CurrentRow.Cells["Назва"].Value?.ToString();
                if (!string.IsNullOrEmpty(continentName))
                {
                    _continentFilter = continentName;
                    _countryFilter = null;
                    _regionFilter = null;
                    txtSearch.Text = string.Empty;
                    btnCountries.PerformClick();
                }
            }
            else if (tag == "countries")
            {
                // Отримуємо назву країни прямо з DataGridView
                var countryName = dataGridView.CurrentRow.Cells["Назва"].Value?.ToString();
                if (!string.IsNullOrEmpty(countryName))
                {
                    _countryFilter = countryName;
                    _regionFilter = null;
                    txtSearch.Text = string.Empty;
                    btnRegions.PerformClick();
                }
            }
            else if (tag == "regions")
            {
                // Отримуємо назву регіону прямо з DataGridView
                var regionName = dataGridView.CurrentRow.Cells["Назва"].Value?.ToString();
                if (!string.IsNullOrEmpty(regionName))
                {
                    _regionFilter = regionName;
                    txtSearch.Text = string.Empty;
                    btnCities.PerformClick();
                }
            }
        }
    }
}