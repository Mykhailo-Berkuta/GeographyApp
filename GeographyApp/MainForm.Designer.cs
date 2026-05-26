namespace GeographyApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            даніToolStripMenuItem = new ToolStripMenuItem();
            пошукToolStripMenuItem = new ToolStripMenuItem();
            картаToolStripMenuItem = new ToolStripMenuItem();
            довідкаToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            btnCities = new Button();
            btnRegions = new Button();
            btnCountries = new Button();
            btnContinents = new Button();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            btnAdd = new ToolStripButton();
            btnEdit = new ToolStripButton();
            btnDelete = new ToolStripButton();
            btnShowMap = new ToolStripButton();
            dataGridView = new DataGridView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, даніToolStripMenuItem, пошукToolStripMenuItem, картаToolStripMenuItem, довідкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1008, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // даніToolStripMenuItem
            // 
            даніToolStripMenuItem.Name = "даніToolStripMenuItem";
            даніToolStripMenuItem.Size = new Size(43, 20);
            даніToolStripMenuItem.Text = "Дані";
            // 
            // пошукToolStripMenuItem
            // 
            пошукToolStripMenuItem.Name = "пошукToolStripMenuItem";
            пошукToolStripMenuItem.Size = new Size(58, 20);
            пошукToolStripMenuItem.Text = "Пошук";
            // 
            // картаToolStripMenuItem
            // 
            картаToolStripMenuItem.Name = "картаToolStripMenuItem";
            картаToolStripMenuItem.Size = new Size(50, 20);
            картаToolStripMenuItem.Text = "Карта";
            // 
            // довідкаToolStripMenuItem
            // 
            довідкаToolStripMenuItem.Name = "довідкаToolStripMenuItem";
            довідкаToolStripMenuItem.Size = new Size(61, 20);
            довідкаToolStripMenuItem.Text = "Довідка";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnCities);
            splitContainer1.Panel1.Controls.Add(btnRegions);
            splitContainer1.Panel1.Controls.Add(btnCountries);
            splitContainer1.Panel1.Controls.Add(btnContinents);
            splitContainer1.Panel1MinSize = 160;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(statusStrip1);
            splitContainer1.Panel2.Controls.Add(toolStrip1);
            splitContainer1.Panel2.Controls.Add(dataGridView);
            splitContainer1.Size = new Size(1008, 537);
            splitContainer1.SplitterDistance = 160;
            splitContainer1.TabIndex = 1;
            // 
            // btnCities
            // 
            btnCities.Dock = DockStyle.Top;
            btnCities.FlatAppearance.BorderSize = 0;
            btnCities.FlatStyle = FlatStyle.Flat;
            btnCities.Location = new Point(0, 135);
            btnCities.Name = "btnCities";
            btnCities.Size = new Size(160, 45);
            btnCities.TabIndex = 3;
            btnCities.Text = "Міста";
            btnCities.UseVisualStyleBackColor = true;
            btnCities.Click += btnCities_Click;
            // 
            // btnRegions
            // 
            btnRegions.Dock = DockStyle.Top;
            btnRegions.FlatAppearance.BorderSize = 0;
            btnRegions.FlatStyle = FlatStyle.Flat;
            btnRegions.Location = new Point(0, 90);
            btnRegions.Name = "btnRegions";
            btnRegions.Size = new Size(160, 45);
            btnRegions.TabIndex = 2;
            btnRegions.Text = "Регіони";
            btnRegions.UseVisualStyleBackColor = true;
            btnRegions.Click += btnRegions_Click;
            // 
            // btnCountries
            // 
            btnCountries.Dock = DockStyle.Top;
            btnCountries.FlatAppearance.BorderSize = 0;
            btnCountries.FlatStyle = FlatStyle.Flat;
            btnCountries.Location = new Point(0, 45);
            btnCountries.Name = "btnCountries";
            btnCountries.Size = new Size(160, 45);
            btnCountries.TabIndex = 1;
            btnCountries.Text = "Країни";
            btnCountries.UseVisualStyleBackColor = true;
            btnCountries.Click += btnCountries_Click;
            // 
            // btnContinents
            // 
            btnContinents.Dock = DockStyle.Top;
            btnContinents.FlatAppearance.BorderSize = 0;
            btnContinents.FlatStyle = FlatStyle.Flat;
            btnContinents.Location = new Point(0, 0);
            btnContinents.Name = "btnContinents";
            btnContinents.Size = new Size(160, 45);
            btnContinents.TabIndex = 0;
            btnContinents.Text = "Материки";
            btnContinents.UseVisualStyleBackColor = true;
            btnContinents.Click += btnContinents_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 515);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(844, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(45, 17);
            statusLabel.Text = "Готово";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnAdd, btnEdit, btnDelete, btnShowMap });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(844, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.ImageTransparentColor = Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(66, 22);
            btnAdd.Text = "Додати";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Image = (Image)resources.GetObject("btnEdit.Image");
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(87, 22);
            btnEdit.Text = "Редагувати";
            btnEdit.Click += this.toolStripButton1_Click;
            // 
            // btnDelete
            // 
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(79, 22);
            btnDelete.Text = "Видалити";
            // 
            // btnShowMap
            // 
            btnShowMap.Image = (Image)resources.GetObject("btnShowMap.Image");
            btnShowMap.ImageTransparentColor = Color.Magenta;
            btnShowMap.Name = "btnShowMap";
            btnShowMap.Size = new Size(72, 22);
            btnShowMap.Text = "На карті";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 0);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(844, 537);
            dataGridView.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 561);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Географічний довідник";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem даніToolStripMenuItem;
        private ToolStripMenuItem пошукToolStripMenuItem;
        private ToolStripMenuItem картаToolStripMenuItem;
        private ToolStripMenuItem довідкаToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Button btnContinents;
        private Button btnCities;
        private Button btnRegions;
        private Button btnCountries;
        private ToolStrip toolStrip1;
        private ToolStripButton btnAdd;
        private ToolStripButton btnEdit;
        private ToolStripButton btnDelete;
        private ToolStripButton btnShowMap;
        private DataGridView dataGridView;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
    }
}
