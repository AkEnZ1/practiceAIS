using DomainModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddForm : Form
    {
        public string EmployeeName { get; private set; }
        public int WorkExperience { get; private set; }
        public VacancyType Vacancy { get; private set; }

        public AddForm()
        {
            InitializeComponent();
        }

        public void SetEmployeeData(string name, int workExp, VacancyType vacancy)
        {
            txtName.Text = name;
            numericWorkExp.Value = workExp;
            comboBoxVacancy.SelectedItem = vacancy;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите имя сотрудника");
                return;
            }

            EmployeeName = txtName.Text;
            WorkExperience = (int)numericWorkExp.Value;
            Vacancy = (VacancyType)comboBoxVacancy.SelectedItem;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
    }
}