namespace GeographyApp.Forms
{
    partial class RegionForm
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
            txtCapital = new TextBox();
            txtType = new TextBox();
            txtPopulation = new TextBox();
            lblError = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // cmbCountry
            // 
            cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Location = new Point(139, 172);
            cmbCountry.Name = "cmbCountry";
            cmbCountry.Size = new Size(180, 23);
            cmbCountry.TabIndex = 30;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(187, 216);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 29;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(72, 216);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 28;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // txtCapital
            // 
            txtCapital.Location = new Point(139, 136);
            txtCapital.Name = "txtCapital";
            txtCapital.Size = new Size(180, 23);
            txtCapital.TabIndex = 26;
            // 
            // txtType
            // 
            txtType.Location = new Point(139, 101);
            txtType.Name = "txtType";
            txtType.Size = new Size(180, 23);
            txtType.TabIndex = 25;
            // 
            // txtPopulation
            // 
            txtPopulation.Location = new Point(139, 66);
            txtPopulation.Name = "txtPopulation";
            txtPopulation.Size = new Size(180, 23);
            txtPopulation.TabIndex = 24;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.DarkRed;
            lblError.Location = new Point(13, 253);
            lblError.Name = "lblError";
            lblError.Size = new Size(320, 20);
            lblError.TabIndex = 23;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Location = new Point(-7, 171);
            label5.Name = "label5";
            label5.Size = new Size(120, 23);
            label5.TabIndex = 21;
            label5.Text = "Країна:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(13, 136);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 20;
            label4.Text = "Адм. центр:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(13, 101);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 19;
            label3.Text = "Тип регіону:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(13, 66);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 18;
            label2.Text = "Населення:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Location = new Point(139, 31);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 23);
            txtName.TabIndex = 17;
            // 
            // label1
            // 
            label1.Location = new Point(13, 31);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 16;
            label1.Text = "Назва:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RegionForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(364, 300);
            Controls.Add(cmbCountry);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtCapital);
            Controls.Add(txtType);
            Controls.Add(txtPopulation);
            Controls.Add(lblError);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Додавання регіону";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCountry;
        private Button btnCancel;
        private Button btnOk;
        private TextBox txtCapital;
        private TextBox txtType;
        private TextBox txtPopulation;
        private Label lblError;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtName;
        private Label label1;
    }
}