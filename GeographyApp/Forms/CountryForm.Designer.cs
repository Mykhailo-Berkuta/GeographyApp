namespace GeographyApp.Forms
{
    partial class CountryForm
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
            label1 = new Label();
            txtName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            lblError = new Label();
            txtPopulation = new TextBox();
            txtArea = new TextBox();
            txtCapital = new TextBox();
            txtGovernment = new TextBox();
            btnOk = new Button();
            btnCancel = new Button();
            cmbContinent = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(25, 22);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Назва:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Location = new Point(151, 22);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 23);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(25, 57);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 2;
            label2.Text = "Населення:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(25, 92);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 3;
            label3.Text = "Площа (км²):";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(25, 127);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 4;
            label4.Text = "Столиця:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Location = new Point(5, 162);
            label5.Name = "label5";
            label5.Size = new Size(120, 23);
            label5.TabIndex = 5;
            label5.Text = "Форма правління:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Location = new Point(25, 197);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 6;
            label6.Text = "Материк:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.DarkRed;
            lblError.Location = new Point(12, 280);
            lblError.Name = "lblError";
            lblError.Size = new Size(320, 20);
            lblError.TabIndex = 7;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPopulation
            // 
            txtPopulation.Location = new Point(151, 57);
            txtPopulation.Name = "txtPopulation";
            txtPopulation.Size = new Size(180, 23);
            txtPopulation.TabIndex = 8;
            // 
            // txtArea
            // 
            txtArea.Location = new Point(151, 92);
            txtArea.Name = "txtArea";
            txtArea.Size = new Size(180, 23);
            txtArea.TabIndex = 9;
            // 
            // txtCapital
            // 
            txtCapital.Location = new Point(151, 127);
            txtCapital.Name = "txtCapital";
            txtCapital.Size = new Size(180, 23);
            txtCapital.TabIndex = 10;
            // 
            // txtGovernment
            // 
            txtGovernment.Location = new Point(151, 162);
            txtGovernment.Name = "txtGovernment";
            txtGovernment.Size = new Size(180, 23);
            txtGovernment.TabIndex = 11;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(71, 243);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 13;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(186, 243);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cmbContinent
            // 
            cmbContinent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContinent.FormattingEnabled = true;
            cmbContinent.Location = new Point(151, 197);
            cmbContinent.Name = "cmbContinent";
            cmbContinent.Size = new Size(180, 23);
            cmbContinent.TabIndex = 15;
            // 
            // CountryForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(364, 341);
            Controls.Add(cmbContinent);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtGovernment);
            Controls.Add(txtCapital);
            Controls.Add(txtArea);
            Controls.Add(txtPopulation);
            Controls.Add(lblError);
            Controls.Add(label6);
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
            Name = "CountryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Додавання країни";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblError;
        private TextBox txtPopulation;
        private TextBox txtArea;
        private TextBox txtCapital;
        private TextBox txtGovernment;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cmbContinent;
    }
}