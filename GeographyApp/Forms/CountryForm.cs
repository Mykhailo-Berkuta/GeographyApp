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
    /// Форма для додавання або редагування країни.
    /// Забезпечує введення назви, населення, площі, столиці, форми правління та вибір материка.
    /// </summary>
    public partial class CountryForm : Form
    {
        private readonly List<Continent> _continents;
        private readonly List<Country>? _existingCountries;
        private readonly string? _originalName;

        /// <summary>
        /// Результат роботи форми — створений або відредагований об'єкт Country.
        /// </summary>
        public Country Result { get; private set; }

        /// <summary>
        /// Конструктор форми додавання країни.
        /// </summary>
        /// <param name="continents">Список доступних материків.</param>
        /// <param name="existingCountries">Необов'язковий список існуючих країн для перевірки унікальності.</param>
        public CountryForm(List<Continent> continents, List<Country>? existingCountries = null)
        {
            InitializeComponent();
            _continents = continents;
            _existingCountries = existingCountries;
            KeyDown += Form_KeyDown;

            // список материків
            cmbContinent.DataSource = _continents;
            cmbContinent.DisplayMember = "Name";
        }

        /// <summary>
        /// Конструктор форми для редагування існуючої країни.
        /// </summary>
        /// <param name="continents">Список материків.</param>
        /// <param name="existingCountries">Список існуючих країн для перевірки унікальності.</param>
        /// <param name="country">Існуючий об'єкт Country для редагування.</param>
        public CountryForm(List<Continent> continents, List<Country>? existingCountries, Country country)
            : this(continents, existingCountries)
        {
            txtName.Text = country.Name;
            txtPopulation.Text = country.Population.ToString();
            txtArea.Text = country.Area.ToString();
            txtCapital.Text = country.Capital;
            txtGovernment.Text = country.GovernmentForm;
            cmbContinent.SelectedItem = _continents
                .FirstOrDefault(c => c.Name == country.Continent.Name);
            Text = "Редагування країни";
            _originalName = country.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            long pop = long.Parse(txtPopulation.Text.Trim());
            double area = double.Parse(txtArea.Text.Trim().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

            Result = new Country(
                txtName.Text.Trim(),
                pop,
                area,
                txtGovernment.Text.Trim(),
                txtCapital.Text.Trim(),
                (Continent)cmbContinent.SelectedItem
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
        /// Перевіряє правильність заповнення полів форми.
        /// </summary>
        private bool ValidateInput()
        {
            lblError.Text = "";

            var name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                lblError.Text = "Введіть назву країни!";
                return false;
            }

            if (_existingCountries != null)
            {
                bool duplicate = _existingCountries.Any(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase) && !string.Equals(c.Name, _originalName, StringComparison.CurrentCultureIgnoreCase));
                if (duplicate)
                {
                    lblError.Text = "Країна з такою назвою вже існує!";
                    return false;
                }
            }

            if (!long.TryParse(txtPopulation.Text.Trim(), out long pop) || pop < 0)
            {
                lblError.Text = "Населення - ціле невід'ємне число!";
                return false;
            }
            if (!double.TryParse(txtArea.Text.Trim().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture, out double area) || area <= 0)
            {
                lblError.Text = "Площа - додатне число!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCapital.Text))
            {
                lblError.Text = "Введіть назву столиці!";
                return false;
            }
            if (cmbContinent.SelectedItem == null)
            {
                lblError.Text = "Оберіть материк!";
                return false;
            }

            return true;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(
                    "Форма додавання/редагування країни.\n\n" +
                    "Назва - довільний текст\n" +
                    "Населення - ціле невід'ємне число\n" +
                    "Площа (км²) - додатне число\n" +
                    "Столиця - довільний текст\n" +
                    "Форма правління - довільний текст\n" +
                    "Материк - оберіть зі списку\n\n" +
                    "Enter - зберегти, Esc - скасувати.",
                    "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}