namespace GeographyApp.Forms
{
    partial class ContinentForm
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
            txtPopulation = new TextBox();
            label3 = new Label();
            txtArea = new TextBox();
            btnOk = new Button();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(120, 23);
            label1.TabIndex = 0;
            label1.Text = "Назва:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Location = new Point(138, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(180, 23);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(12, 55);
            label2.Name = "label2";
            label2.Size = new Size(120, 23);
            label2.TabIndex = 2;
            label2.Text = "Населення:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPopulation
            // 
            txtPopulation.Location = new Point(138, 55);
            txtPopulation.Name = "txtPopulation";
            txtPopulation.Size = new Size(180, 23);
            txtPopulation.TabIndex = 3;
            // 
            // label3
            // 
            label3.Location = new Point(12, 90);
            label3.Name = "label3";
            label3.Size = new Size(120, 23);
            label3.TabIndex = 4;
            label3.Text = "Площа (км²):";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtArea
            // 
            txtArea.Location = new Point(138, 90);
            txtArea.Name = "txtArea";
            txtArea.Size = new Size(180, 23);
            txtArea.TabIndex = 5;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(60, 140);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 6;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(175, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.DarkRed;
            lblError.Location = new Point(12, 185);
            lblError.Name = "lblError";
            lblError.Size = new Size(330, 23);
            lblError.TabIndex = 8;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ContinentForm
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(364, 261);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtArea);
            Controls.Add(label3);
            Controls.Add(txtPopulation);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ContinentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Додавання материка";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private Label label2;
        private TextBox txtPopulation;
        private Label label3;
        private TextBox txtArea;
        private Button btnOk;
        private Button btnCancel;
        private Label lblError;
    }
}