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

            try
            {
                _dataManager.Load();
            }
            catch (Exception ex)
            {
                // Некритична помилка завантаження — показуємо дружнє повідомлення і завантажуємо тестові дані
                MessageBox.Show("Не вдалося завантажити дані з файлу. Буде завантажено тестові дані.\n\nДеталі: " + ex.Message,
                    "Помилка завантаження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    _dataManager.LoadSampleData();
                }
                catch
                {
                    // Якщо і тестові дані не завантажуються — очистимо існуючі колекції
                    _dataManager.Continents.Clear();
                    _dataManager.Countries.Clear();
                    _dataManager.Regions.Clear();
                    _dataManager.Cities.Clear();
                }
            }

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

        // Helper: safely get cell value by column header name
        private string? GetSelectedCellValue(string columnName)
        {
            try
            {
                if (dataGridView.CurrentRow == null) return null;
                var cell = dataGridView.CurrentRow.Cells[columnName];
                return cell?.Value?.ToString();
            }
            catch
            {
                return null;
            }
        }
        
        // Helper: find index in list by name (case-insensitive)
        private int FindIndexByName<T>(System.Collections.Generic.List<T> list, string name) where T : GeographicObject
        {
            return list.FindIndex(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
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
                    MessageBox.Show("Спочатку додайте хоч би одну країну!",
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
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Будь ласка, виберіть запис для редагування.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var tag = dataGridView.Tag?.ToString();
                var name = GetSelectedCellValue("Назва");
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Не вдалося отримати назву вибраного запису.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tag == "continents")
                {
                    int idx = FindIndexByName(_dataManager.Continents, name);
                    if (idx < 0)
                    {
                        MessageBox.Show("Вибраний материк не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var continent = _dataManager.Continents[idx];
                    using var form = new Forms.ContinentForm(continent);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        _dataManager.Continents[idx] = form.Result;
                        ShowContinents();
                    }
                }
                else if (tag == "countries")
                {
                    int idx = FindIndexByName(_dataManager.Countries, name);
                    if (idx < 0)
                    {
                        MessageBox.Show("Вибрану країну не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var country = _dataManager.Countries[idx];
                    using var form = new Forms.CountryForm(_dataManager.Continents, country);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        _dataManager.Countries[idx] = form.Result;
                        ShowCountries();
                    }
                }
                else if (tag == "regions")
                {
                    int idx = FindIndexByName(_dataManager.Regions, name);
                    if (idx < 0)
                    {
                        MessageBox.Show("Вибраний регіон не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var region = _dataManager.Regions[idx];
                    using var form = new Forms.RegionForm(_dataManager.Countries, region);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        _dataManager.Regions[idx] = form.Result;
                        ShowRegions();
                    }
                }
                else if (tag == "cities")
                {
                    int idx = FindIndexByName(_dataManager.Cities, name);
                    if (idx < 0)
                    {
                        MessageBox.Show("Вибране місто не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var city = _dataManager.Cities[idx];
                    using var form = new Forms.CityForm(_dataManager.Countries, _dataManager.Regions, city);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        _dataManager.Cities[idx] = form.Result;
                        ShowCities();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при редагуванні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Будь ласка, виберіть запис для видалення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var name = GetSelectedCellValue("Назва");
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Не вдалося визначити вибраний запис.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show(
                    "Ви впевнені, що хочете видалити цей запис? Ця дія необоротна.",
                    "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var tag = dataGridView.Tag?.ToString();
                if (tag == "continents")
                {
                    int idx = FindIndexByName(_dataManager.Continents, name);
                    if (idx < 0) { MessageBox.Show("Материк не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    var dependentCountries = _dataManager.Countries.Count(c => c.Continent.Name == name);
                    if (dependentCountries > 0)
                    {
                        MessageBox.Show($"Неможливо видалити материк. Існує {dependentCountries} країн(и), що належать цьому материку. Спочатку видаліть їх.",
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _dataManager.Continents.RemoveAt(idx);
                    ShowContinents();
                }
                else if (tag == "countries")
                {
                    int idx = FindIndexByName(_dataManager.Countries, name);
                    if (idx < 0) { MessageBox.Show("Країну не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    var dependentRegions = _dataManager.Regions.Count(r => r.Country.Name == name);
                    var dependentCities = _dataManager.Cities.Count(c => c.Country.Name == name);
                    if (dependentRegions > 0 || dependentCities > 0)
                    {
                        MessageBox.Show($"Неможливо видалити країну. Існує {dependentRegions} регіон(ів) та {dependentCities} міст(а), що належать цій країні. Спочатку видаліть їх.",
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _dataManager.Countries.RemoveAt(idx);
                    ShowCountries();
                }
                else if (tag == "regions")
                {
                    int idx = FindIndexByName(_dataManager.Regions, name);
                    if (idx < 0) { MessageBox.Show("Регіон не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    var dependentCities = _dataManager.Cities.Count(c => c.Region.Name == name);
                    if (dependentCities > 0)
                    {
                        MessageBox.Show($"Неможливо видалити регіон. Існує {dependentCities} міст(а), що належать цьому регіону. Спочатку видаліть їх.",
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _dataManager.Regions.RemoveAt(idx);
                    ShowRegions();
                }
                else if (tag == "cities")
                {
                    int idx = FindIndexByName(_dataManager.Cities, name);
                    if (idx < 0) { MessageBox.Show("Місто не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    _dataManager.Cities.RemoveAt(idx);
                    ShowCities();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при видаленні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Виберіть запис у таблиці.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var tag = dataGridView.Tag?.ToString();
                var name = GetSelectedCellValue("Назва");
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Не вдалося визначити назву об'єкту для карти.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = string.Empty;
                if (tag == "continents")
                {
                    query = name;
                }
                else if (tag == "countries")
                {
                    query = name;
                }
                else if (tag == "regions")
                {
                    var region = _dataManager.Regions.FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
                    if (region == null) { MessageBox.Show("Регіон не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    query = $"{region.Name}, {region.Country.Name}";
                }
                else if (tag == "cities")
                {
                    var city = _dataManager.Cities.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));
                    if (city == null) { MessageBox.Show("Місто не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    query = $"{city.Name}, {city.Country.Name}";
                }

                if (string.IsNullOrEmpty(query))
                {
                    MessageBox.Show("Неможливо відкрити карту для цього запису.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string url = $"https://www.google.com/maps/search/{Uri.EscapeDataString(query)}";
                using var mapForm = new Forms.MapForm(url, query);
                mapForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося відкрити карту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //збереження при закритті форми
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try
            {
                _dataManager.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося зберегти дані: {ex.Message}", "Помилка збереження", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void menuSave_Click(object sender, EventArgs e)
        {
            try
            {
                _dataManager.Save();
                MessageBox.Show("Дані збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося зберегти дані: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuLoad_Click(object sender, EventArgs e)
        {
            try
            {
                _dataManager.Load();
                RefreshCurrentView();
                MessageBox.Show("Дані завантажено!", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося завантажити дані: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                "Програма для зберігання та перегляду географічних об'єктів: материків, країн, регіонів та міст.\n\n" +
                "Особливості:\n" +
                "- Додавання / Редагування / Видалення записів\n" +
                "- Пошук по поточному розділу (натисніть Enter або кнопку пошуку)\n" +
                "- Меню «Пошук» фокусує поле пошуку, є кнопка на панелі інструментів\n" +
                "- Сортування (За назвою, За населенням, За площею) у панелі сортування\n" +
                "- Сортування імен виконується з урахуванням культури, числові поля показують найбільші зверху\n" +
                "- Подвійний клік: материк → показати країни цього материку; країна → показати її регіони; регіон → показати міста\n" +
                "- Відкрити на карті (Google Maps) для вибраного запису\n" +
                "- Збереження / Завантаження у JSON-файл та статистика населення\n\n" +
                "Автор: Беркута М.С.\n" +
                "ХНУРЕ, 2026",
                "Про програму",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShowHelp()
        {
            MessageBox.Show(
                "Довідка — короткий огляд функцій\n\n" +
                "Навігація:\n" +
                "- Використовуйте кнопки ліворуч для перемикання між розділами (Материки, Країни, Регіони, Міста).\n\n" +
                "Пошук:\n" +
                "- Введіть текст у поле пошуку на панелі і натисніть Enter або кнопку пошуку.\n" +
                "- Пункт меню 'Пошук' та кнопка 'Пошук' фокусують поле для вводу.\n\n" +
                "Сортування:\n" +
                "- Кнопка сортування на панелі: За назвою / За населенням / За площею.\n" +
                "- Імена сортуються з урахуванням поточної культури; числові поля сортуються від більшого до меншого.\n\n" +
                "Фільтрація по батьківському об'єкту:\n" +
                "- Подвійний клік на материк показує тільки його країни.\n" +
                "- Подвійний клік на країну показує тільки її регіони.\n" +
                "- Подвійний клік на регіон показує тільки його міста.\n\n" +
                "Інше:\n" +
                "- Кнопка 'На карті' відкриває Google Maps для вибраного запису.\n" +
                "- Файл → Зберегти / Завантажити для роботи з JSON-файлом.\n" +
                "- Кнопки на панелі: Довідка та Про програму відкривають ці діалоги.\n\n" +
                "Натисніть F1 або кнопку 'Довідка' для цієї довідки.",
                "Довідка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ShowHelp();
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
        
        private void menuHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }
    }
}