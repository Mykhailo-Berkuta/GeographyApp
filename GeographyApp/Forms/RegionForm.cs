using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeographyApp.Models;

namespace GeographyApp.Forms
{
    /// Форма для додавання або редагування регіону
    public partial class RegionForm : Form
    {
        private readonly List<Country> _countries;

        public Models.Region Result { get; private set; }

        public RegionForm(List<Country> countries)
        {
            InitializeComponent();
            _countries = countries;
            cmbCountry.DataSource = _countries;
            cmbCountry.DisplayMember = "Name";
        }

        /// Конструктор для редагування існуючого регіону
        public RegionForm(List<Country> countries, Models.Region region)
            : this(countries)
        {
            txtName.Text = region.Name;
            txtPopulation.Text = region.Population.ToString();
            txtType.Text = region.RegionType;
            txtCapital.Text = region.Capital;
            cmbCountry.SelectedItem = _countries
                .FirstOrDefault(c => c.Name == region.Country.Name);
            Text = "Редагування регіону";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Result = new Models.Region(
                txtName.Text.Trim(),
                long.Parse(txtPopulation.Text.Trim()),
                txtType.Text.Trim(),
                txtCapital.Text.Trim(),
                (Country)cmbCountry.SelectedItem
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
                lblError.Text = "Введіть назву регіону!";
                return false;
            }
            if (!long.TryParse(txtPopulation.Text.Trim(), out long pop) || pop < 0)
            {
                lblError.Text = "Населення - ціле невід'ємне число!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                lblError.Text = "Введіть тип регіону!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCapital.Text))
            {
                lblError.Text = "Введіть адміністративний центр!";
                return false;
            }
            if (cmbCountry.SelectedItem == null)
            {
                lblError.Text = "Оберіть країну!";
                return false;
            }

            return true;
        }
    }
}