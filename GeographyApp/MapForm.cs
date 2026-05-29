using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace GeographyApp.Forms
{
    /// <summary>
    /// Форма, що відображає карту (вбудований WebView2) за переданою URL-адресою.
    /// Використовується для показу місця об'єкта на Google Maps.
    /// </summary>
    public partial class MapForm : Form
    {
        private readonly string _url;

        /// <summary>
        /// Створює форму карти для вказаної URL та встановлює заголовок вікна.
        /// </summary>
        /// <param name="url">URL для відкриття у WebView2.</param>
        /// <param name="title">Заголовок вікна карти.</param>
        public MapForm(string url, string title)
        {
            InitializeComponent();
            _url = url;
            Text = title;
            Load += MapForm_Load;
        }

        /// <summary>
        /// Обробник завантаження форми — ініціалізує WebView2 і встановлює джерело сторінки.
        /// </summary>
        private async void MapForm_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.Source = new Uri(_url);
        }

    }
}