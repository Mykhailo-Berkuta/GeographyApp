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
    /// Форма для додавання або редагування країни.
    public partial class CountryForm : Form
    {
        private readonly List<Continent> _continents;

        public Country Result { get; private set; }

        public CountryForm(List<Continent> continents)
        {
            InitializeComponent();
            _continents = continents;

            // список материків
            cmbContinent.DataSource = _continents;
            cmbContinent.DisplayMember = "Name";
        }

        /// Конструктор для редагування існуючої країни
        public CountryForm(List<Continent> continents, Country country)
            : this(continents)
        {
            txtName.Text = country.Name;
            txtPopulation.Text = country.Population.ToString();
            txtArea.Text = country.Area.ToString();
            txtCapital.Text = country.Capital;
            txtGovernment.Text = country.GovernmentForm;
            cmbContinent.SelectedItem = _continents
                .FirstOrDefault(c => c.Name == country.Continent.Name);
            Text = "Редагування країни";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Result = new Country(
                txtName.Text.Trim(),
                long.Parse(txtPopulation.Text.Trim()),
                double.Parse(txtArea.Text.Trim()),
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

        /// Перевіряє правильність заповнення полів форми
        private bool ValidateInput()
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblError.Text = "Введіть назву країни!";
                return false;
            }
            if (!long.TryParse(txtPopulation.Text.Trim(), out long pop) || pop < 0)
            {
                lblError.Text = "Населення — ціле невід'ємне число!";
                return false;
            }
            if (!double.TryParse(txtArea.Text.Trim(), out double area) || area <= 0)
            {
                lblError.Text = "Площа — додатне число!";
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
    }
}