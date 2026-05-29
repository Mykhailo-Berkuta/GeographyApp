using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeographyApp.Models;

namespace GeographyApp.Forms
{
    /// <summary>
    /// Форма для додавання або редагування регіону.
    /// Забезпечує введення назви, населення, типу регіону, адміністративного центру та вибір країни.
    /// </summary>
    public partial class RegionForm : Form
    {
        private readonly List<Country> _countries;
        private readonly List<Models.Region>? _existingRegions;
        private readonly string? _originalName;

        /// <summary>
        /// Результат роботи форми — створений або відредагований об'єкт Region.
        /// </summary>
        public Models.Region Result { get; private set; }

        /// <summary>
        /// Конструктор форми додавання регіону.
        /// </summary>
        /// <param name="countries">Список доступних країн для вибору.</param>
        /// <param name="existingRegions">Необов'язковий список існуючих регіонів для перевірки унікальності.</param>
        public RegionForm(List<Country> countries, List<Models.Region>? existingRegions = null)
        {
            InitializeComponent();
            _countries = countries;
            _existingRegions = existingRegions;
            cmbCountry.DataSource = _countries;
            cmbCountry.DisplayMember = "Name";
            KeyDown += Form_KeyDown;
        }

        /// <summary>
        /// Конструктор форми для редагування існуючого регіону.
        /// </summary>
        /// <param name="countries">Список доступних країн для вибору.</param>
        /// <param name="existingRegions">Необов'язковий список існуючих регіонів для перевірки унікальності.</param>
        /// <param name="region">Існуючий об'єкт Region для редагування.</param>
        public RegionForm(List<Country> countries, List<Models.Region>? existingRegions, Models.Region region)
            : this(countries, existingRegions)
        {
            txtName.Text = region.Name;
            txtPopulation.Text = region.Population.ToString();
            txtType.Text = region.RegionType;
            txtCapital.Text = region.Capital;
            cmbCountry.SelectedItem = _countries
                .FirstOrDefault(c => c.Name == region.Country.Name);
            Text = "Редагування регіону";
            _originalName = region.Name;
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

        /// <summary>
        /// Перевіряє правильність введених даних у формі.
        /// </summary>
        private bool ValidateInput()
        {
            lblError.Text = "";

            var name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                lblError.Text = "Введіть назву регіону!";
                return false;
            }

            if (_existingRegions != null)
            {
                bool duplicate = _existingRegions.Any(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase) && !string.Equals(r.Name, _originalName, StringComparison.CurrentCultureIgnoreCase));
                if (duplicate)
                {
                    lblError.Text = "Регіон з такою назвою вже існує!";
                    return false;
                }
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

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(
                    "Форма додавання/редагування регіону.\n\n" +
                    "Назва - довільний текст\n" +
                    "Населення - ціле невід'ємне число\n" +
                    "Тип регіону - наприклад: область, штат\n" +
                    "Адм. центр - довільний текст\n" +
                    "Країна - оберіть зі списку\n\n" +
                    "Enter - зберегти, Esc - скасувати.",
                    "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}