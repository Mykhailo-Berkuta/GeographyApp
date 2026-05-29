using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeographyApp.Models;
using Region = GeographyApp.Models.Region;

namespace GeographyApp.Forms
{
    public partial class CityForm : Form
    {
        private readonly List<Country> _countries;
        private readonly List<Region> _regions;

        public City Result { get; private set; }

        public CityForm(List<Country> countries, List<Region> regions)
        {
            InitializeComponent();
            _countries = countries;
            _regions = regions;
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

        /// Конструктор для редагування існуючого міста
        public CityForm(List<Country> countries, List<Region> regions, City city)
            : this(countries, regions)
        {
            txtName.Text = city.Name;
            txtPopulation.Text = city.Population.ToString();
            txtLatitude.Text = city.Latitude.ToString("F4").Replace(".", ",");
            txtLongitude.Text = city.Longitude.ToString("F4").Replace(".", ",");
            cmbCountry.SelectedItem = _countries
                .FirstOrDefault(c => c.Name == city.Country.Name);
            cmbRegion.SelectedItem = _regions
                .FirstOrDefault(r => r.Name == city.Region.Name);
            Text = "Редагування міста";
        }

        /// Оновлює список регіонів відповідно до обраної країни
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

        /// Перевірка
        private bool ValidateInput()
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
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

            return true;
        }

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