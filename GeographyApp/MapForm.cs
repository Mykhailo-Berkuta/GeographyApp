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
    public partial class MapForm : Form
    {
        private readonly string _url;

        public MapForm(string url, string title)
        {
            InitializeComponent();
            _url = url;
            Text = title;
            Load += MapForm_Load;
        }

        private async void MapForm_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.Source = new Uri(_url);
        }

    }
}