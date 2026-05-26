namespace GeographyApp.Forms
{
    partial class CityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbCountry = new ComboBox();
            btnCancel = new Button();
            btnOk = new Button();
            txtLongitude = new TextBox();
            txtLatitude = new TextBox();
            txtPopulation = new TextBox();
            lblError = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            label1 = new Label();
            cmbRegion = new ComboBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // cmbCountry
            // 
            cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Location = new Point(139, 169);
            cmbCountry.Name = "cmbCountry";
            cmbCountry.Size = new Size(180, 23);
            cmbCountry.TabIndex = 43;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(187, 252);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 42;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(72, 252);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 41;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // txtLongitude
            // 
            txtLongitude.Location = new Point(139, 133);
            txtLongitude.Name = "txtLongitude";
            txtLongitude.Size = new Size(180, 23);
            txtLongitude.TabIndex = 40;
            // 
            // txtLatitude
            // 
            txtLatitude.Location = new Point(139, 98);
            txtLatitude.Name = "txtLatitude";
            txtLatitude.Size = new Size(180, 23);
            txtLatitude.TabIndex = 39;
            // 
            // txtPopulation
            // 
            txtPopulation.Location = new Point(139, 63);
            txtPopulation.Name = "txtPopulation";
            txtPopulation.Size = new Size(180, 23);
            txtPopulation.TabIndex = 38;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.DarkRed;
            lblError.Location = new Point(13, 289);
            lblError.Name = "lblError";
            lblError.Size = new Size(320, 20);
            lblError.TabIndex = 37;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Location = new Point(-7, 168);
            label5.Name = "label5";
            label5.Size = new Size(120, 23);
            label5.TabIndex = 36;
            label5.Text = "Країна:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(13, 133);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 35;
            label4.Text = "Довгота:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(13, 98);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 34;
            label3.Text = "Широта:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(13, 63);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 33;
            label2.Text = "Населення:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Location = new Point(139, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 23);
            txtName.TabIndex = 32;
            // 
            // label1
            // 
            label1.Location = new Point(13, 28);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 31;
            label1.Text = "Назва:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbRegion
            // 
            cmbRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRegion.FormattingEnabled = true;
            cmbRegion.Location = new Point(139, 207);
            cmbRegion.Name = "cmbRegion";
            cmbRegion.Size = new Size(180, 23);
            cmbRegion.TabIndex = 45;
            // 
            // label6
            // 
            label6.Location = new Point(-7, 206);
            label6.Name = "label6";
            label6.Size = new Size(120, 23);
            label6.TabIndex = 44;
            label6.Text = "Регіон:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 331);
            Controls.Add(cmbRegion);
            Controls.Add(label6);
            Controls.Add(cmbCountry);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtLongitude);
            Controls.Add(txtLatitude);
            Controls.Add(txtPopulation);
            Controls.Add(lblError);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CityForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Додавання міста";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCountry;
        private Button btnCancel;
        private Button btnOk;
        private TextBox txtLongitude;
        private TextBox txtLatitude;
        private TextBox txtPopulation;
        private Label lblError;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtName;
        private Label label1;
        private ComboBox cmbRegion;
        private Label label6;
    }
}