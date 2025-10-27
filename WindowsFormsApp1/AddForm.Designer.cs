namespace WindowsFormsApp1
{
    partial class AddForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.numericWorkExp = new System.Windows.Forms.NumericUpDown();
            this.comboBoxVacancy = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblWorkExp = new System.Windows.Forms.Label();
            this.lblVacancy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericWorkExp)).BeginInit();
            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(32, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Имя:";

            // lblWorkExp
            this.lblWorkExp.AutoSize = true;
            this.lblWorkExp.Location = new System.Drawing.Point(12, 45);
            this.lblWorkExp.Name = "lblWorkExp";
            this.lblWorkExp.Size = new System.Drawing.Size(85, 13);
            this.lblWorkExp.TabIndex = 1;
            this.lblWorkExp.Text = "Опыт работы:";

            // lblVacancy
            this.lblVacancy.AutoSize = true;
            this.lblVacancy.Location = new System.Drawing.Point(12, 75);
            this.lblVacancy.Name = "lblVacancy";
            this.lblVacancy.Size = new System.Drawing.Size(68, 13);
            this.lblVacancy.TabIndex = 2;
            this.lblVacancy.Text = "Должность:";

            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 3;

            // numericWorkExp
            this.numericWorkExp.Location = new System.Drawing.Point(100, 42);
            this.numericWorkExp.Name = "numericWorkExp";
            this.numericWorkExp.Size = new System.Drawing.Size(100, 20);
            this.numericWorkExp.TabIndex = 4;
            this.numericWorkExp.Minimum = 0;
            this.numericWorkExp.Maximum = 50;

            // comboBoxVacancy
            this.comboBoxVacancy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVacancy.FormattingEnabled = true;
            this.comboBoxVacancy.Location = new System.Drawing.Point(100, 72);
            this.comboBoxVacancy.Name = "comboBoxVacancy";
            this.comboBoxVacancy.Size = new System.Drawing.Size(200, 21);
            this.comboBoxVacancy.TabIndex = 5;

            // Заполняем комбобокс значениями
            this.comboBoxVacancy.Items.AddRange(new object[] {
                DomainModel.VacancyType.Head,
                DomainModel.VacancyType.Manager,
                DomainModel.VacancyType.Intern
            });
            this.comboBoxVacancy.SelectedIndex = 0;

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(100, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(185, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // AddForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 150);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboBoxVacancy);
            this.Controls.Add(this.numericWorkExp);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblVacancy);
            this.Controls.Add(this.lblWorkExp);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить сотрудника";
            ((System.ComponentModel.ISupportInitialize)(this.numericWorkExp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numericWorkExp;
        private System.Windows.Forms.ComboBox comboBoxVacancy;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblWorkExp;
        private System.Windows.Forms.Label lblVacancy;
    }
}