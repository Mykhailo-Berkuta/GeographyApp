using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeographyApp.Models;
using Region = GeographyApp.Models.Region;

namespace GeographyApp.Forms
{
    /// <summary>
    /// Форма для додавання або редагування міста. Забезпечує введення назви, населення,
    /// вибір країни та регіону, а також координат (широта/довгота).
    /// </summary>
    public partial class CityForm : Form
    {
        private readonly List<Country> _countries;
        private readonly List<Region> _regions;
        private readonly List<City>? _existingCities;
        private readonly string? _originalName;

        /// <summary>
        /// Результат роботи форми — створений або відредагований об'єкт City.
        /// Після успішного завершення діалогу (DialogResult.OK) міститься кінцевий об'єкт.
        /// </summary>
        public City Result { get; private set; }

        /// <summary>
        /// Конструктор форми додавання міста.
        /// </summary>
        /// <param name="countries">Список доступних країн для вибору.</param>
        /// <param name="regions">Список доступних регіонів (використовується для фільтрації за країною).</param>
        /// <param name="existingCities">Необов'язковий список існуючих міст для перевірки унікальності.</param>
        public CityForm(List<Country> countries, List<Region> regions, List<City>? existingCities = null)
        {
            InitializeComponent();
            _countries = countries;
            _regions = regions;
            _existingCities = existingCities;
            KeyDown += Form_KeyDown;

            cmbCountry.DataSource = _countries;
            cmbCountry.DisplayMember = "Name";

            // При зміні країни оновлюємо список регіонів
            cmbCountry.SelectedIndexChanged += (s, e) => UpdateRegions();
            UpdateRegions();
            
            // За замовчуванням координати (0, 0)
            txtLatitude.Text = "0";
            txtLongitude.Text = "0";
        }

        /// <summary>
        /// Конструктор форми для редагування існуючого міста.
        /// </summary>
        /// <param name="countries">Список доступних країн для вибору.</param>
        /// <param name="regions">Список доступних регіонів.</param>
        /// <param name="existingCities">Необов'язковий список існуючих міст для перевірки унікальності.</param>
        /// <param name="city">Існуючий об'єкт City для редагування.</param>
        public CityForm(List<Country> countries, List<Region> regions, List<City>? existingCities, City city)
            : this(countries, regions, existingCities)
        {
            txtName.Text = city.Name;
            txtPopulation.Text = city.Population.ToString();
            txtLatitude.Text = city.Latitude.ToString("F4").Replace(".", ", ");
            txtLongitude.Text = city.Longitude.ToString("F4").Replace(".", ", ");
            cmbCountry.SelectedItem = _countries
                .FirstOrDefault(c => c.Name == city.Country.Name);
            cmbRegion.SelectedItem = _regions
                .FirstOrDefault(r => r.Name == city.Region.Name);
            Text = "Редагування міста";
            _originalName = city.Name;
        }

        /// <summary>
        /// Оновлює список регіонів у випадаючому списку згідно з вибраною країною.
        /// </summary>
        private void UpdateRegions()
        {
            if (cmbCountry.SelectedItem is Country selectedCountry)
            {
                var filteredRegions = _regions
                    .Where(r => r.Country.Name == selectedCountry.Name)
                    .ToList();
                cmbRegion.DataSource = filteredRegions;
                cmbRegion.DisplayMember = "Name";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Result = new City(
                txtName.Text.Trim(),
                long.Parse(txtPopulation.Text.Trim()),
                (Region)cmbRegion.SelectedItem,
                (Country)cmbCountry.SelectedItem,
                double.Parse(txtLatitude.Text.Trim().Replace(",", "."), 
                    System.Globalization.CultureInfo.InvariantCulture),
                double.Parse(txtLongitude.Text.Trim().Replace(",", "."), 
                    System.Globalization.CultureInfo.InvariantCulture)
            );

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Перевіряє правильність введених даних у формі.
        /// Показує повідомлення про помилку в lblError і повертає false при невірних даних.
        /// </summary>
        /// <returns>true якщо всі поля валідні, інакше false.</returns>
        private bool ValidateInput()
        {
            lblError.Text = "";

            var name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                lblError.Text = "Введіть назву міста!";
                return false;
            }
            if (!long.TryParse(txtPopulation.Text.Trim(), out long pop) || pop < 0)
            {
                lblError.Text = "Населення - ціле невід'ємне число!";
                return false;
            }
            if (!double.TryParse(txtLatitude.Text.Trim().Replace(",", "."), 
                System.Globalization.CultureInfo.InvariantCulture, out double lat) ||
                lat < -90 || lat > 90)
            {
                lblError.Text = "Широта - число від -90 до 90!";
                return false;
            }
            if (!double.TryParse(txtLongitude.Text.Trim().Replace(",", "."), 
                System.Globalization.CultureInfo.InvariantCulture, out double lon) ||
                lon < -180 || lon > 180)
            {
                lblError.Text = "Довгота - число від -180 до 180!";
                return false;
            }
            if (cmbCountry.SelectedItem == null)
            {
                lblError.Text = "Оберіть країну!";
                return false;
            }
            if (cmbRegion.SelectedItem == null)
            {
                lblError.Text = "Оберіть регіон!";
                return false;
            }

            // Унікальність: немає двох міст з однаковою назвою в одній країні
            if (_existingCities != null)
            {
                var selectedCountry = (Country)cmbCountry.SelectedItem;
                bool duplicate = _existingCities.Any(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase)
                                                          && c.Country.Name == selectedCountry.Name
                                                          && !string.Equals(c.Name, _originalName, StringComparison.CurrentCultureIgnoreCase));
                if (duplicate)
                {
                    lblError.Text = "У цій країні вже існує місто з такою назвою!";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Обробник натискання клавіші F1 — показує довідку по формі.
        /// </summary>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(
                    "Форма додавання/редагування міста.\n\n" +
                    "Назва - довільний текст\n" +
                    "Населення - ціле невід'ємне число\n" +
                    "Країна - оберіть зі списку\n" +
                    "Регіон - оберіть зі списку (залежить від країни)\n" +
                    "Широта - число від -90 до 90\n" +
                    "Довгота - число від -180 до 180\n\n" +
                    "Enter - зберегти, Esc - скасувати.",
                    "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}