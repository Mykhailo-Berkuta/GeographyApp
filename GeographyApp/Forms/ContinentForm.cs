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
    public partial class ContinentForm : Form
    {
        public Continent Result { get; private set; }

        private readonly List<Continent>? _existingContinents;
        private readonly string? _originalName;

        public ContinentForm(List<Continent>? existingContinents = null)
        {
            InitializeComponent();
            KeyDown += Form_KeyDown;
            _existingContinents = existingContinents;
        }

        /// Конструктор для редагування існуючого материка
        public ContinentForm(Continent continent, List<Continent>? existingContinents = null) : this(existingContinents)
        {
            txtName.Text = continent.Name;
            txtPopulation.Text = continent.Population.ToString();
            txtArea.Text = continent.Area.ToString();
            Text = "Редагування материка";
            _originalName = continent.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Перевірка введених даних
            if (!ValidateInput()) return;

            // Використовуємо вже перевірені значення
            long pop = long.Parse(txtPopulation.Text.Trim());
            double area = double.Parse(txtArea.Text.Trim().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

            Result = new Continent(
                txtName.Text.Trim(),
                pop,
                area
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

            var name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                lblError.Text = "Введіть назву материка!";
                return false;
            }

            if (_existingContinents != null)
            {
                bool duplicate = _existingContinents.Any(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase) && !string.Equals(c.Name, _originalName, StringComparison.CurrentCultureIgnoreCase));
                if (duplicate)
                {
                    lblError.Text = "Материк з такою назвою вже існує!";
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

            return true;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(
                    "Форма додавання/редагування материка.\n\n" +
                    "Назва - довільний текст\n" +
                    "Населення - ціле невід'ємне число\n" +
                    "Площа (км²) - додатне число\n\n" +
                    "Enter - зберегти, Esc - скасувати.",
                    "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}