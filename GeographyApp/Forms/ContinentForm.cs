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
    public partial class ContinentForm : Form
    {
        public Continent Result { get; private set; }

        public ContinentForm()
        {
            InitializeComponent();
        }

        /// Конструктор для редагування існуючого материка
        public ContinentForm(Continent continent) : this()
        {
            txtName.Text = continent.Name;
            txtPopulation.Text = continent.Population.ToString();
            txtArea.Text = continent.Area.ToString();
            Text = "Редагування материка";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Перевірка введених даних
            if (!ValidateInput()) return;

            Result = new Continent(
                txtName.Text.Trim(),
                long.Parse(txtPopulation.Text.Trim()),
                double.Parse(txtArea.Text.Trim())
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
                lblError.Text = "Введіть назву материка!";
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

            return true;
        }
    }
}