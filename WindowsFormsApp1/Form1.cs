using System;
using System.Windows.Forms;
using LogicAndModel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Logic logic = new Logic();
        private ListBox listBoxEmployees;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCalculateSalary;
        private Button btnRefresh;
        private Button btnAddExperience;
        private TextBox txtIndex;
        private Button btnFindByIndex;
        private Label lblIndex;

        public Form1()
        {
            InitializeComponent();
            RefreshEmployeeList();
        }

        private void InitializeComponent()
        {
            this.listBoxEmployees = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCalculateSalary = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddExperience = new System.Windows.Forms.Button();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.btnFindByIndex = new System.Windows.Forms.Button();
            this.lblIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxEmployees
            // 
            this.listBoxEmployees.FormattingEnabled = true;
            this.listBoxEmployees.Location = new System.Drawing.Point(12, 12);
            this.listBoxEmployees.Name = "listBoxEmployees";
            this.listBoxEmployees.Size = new System.Drawing.Size(540, 199);
            this.listBoxEmployees.TabIndex = 0;
            this.listBoxEmployees.SelectedIndexChanged += new System.EventHandler(this.listBoxEmployees_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(118, 220);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Изменить";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(224, 220);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCalculateSalary
            // 
            this.btnCalculateSalary.Location = new System.Drawing.Point(330, 220);
            this.btnCalculateSalary.Name = "btnCalculateSalary";
            this.btnCalculateSalary.Size = new System.Drawing.Size(120, 30);
            this.btnCalculateSalary.TabIndex = 4;
            this.btnCalculateSalary.Text = "Рассчитать зарплату";
            this.btnCalculateSalary.Click += new System.EventHandler(this.btnCalculateSalary_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Обновить список";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddExperience
            // 
            this.btnAddExperience.Location = new System.Drawing.Point(456, 220);
            this.btnAddExperience.Name = "btnAddExperience";
            this.btnAddExperience.Size = new System.Drawing.Size(100, 30);
            this.btnAddExperience.TabIndex = 5;
            this.btnAddExperience.Text = "Добавить стаж";
            this.btnAddExperience.Click += new System.EventHandler(this.btnAddExperience_Click);
            // 
            // txtIndex
            // 
            this.txtIndex.Location = new System.Drawing.Point(328, 269);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(50, 20);
            this.txtIndex.TabIndex = 7;
            // 
            // btnFindByIndex
            // 
            this.btnFindByIndex.Location = new System.Drawing.Point(194, 260);
            this.btnFindByIndex.Name = "btnFindByIndex";
            this.btnFindByIndex.Size = new System.Drawing.Size(130, 30);
            this.btnFindByIndex.TabIndex = 8;
            this.btnFindByIndex.Text = "Найти по индексу";
            this.btnFindByIndex.Click += new System.EventHandler(this.btnFindByIndex_Click);
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(330, 253);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(48, 13);
            this.lblIndex.TabIndex = 9;
            this.lblIndex.Text = "Индекс:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(564, 301);
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.btnFindByIndex);
            this.Controls.Add(this.txtIndex);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAddExperience);
            this.Controls.Add(this.btnCalculateSalary);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBoxEmployees);
            this.Name = "Form1";
            this.Text = "Управление сотрудниками";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void RefreshEmployeeList()
        {
            listBoxEmployees.Items.Clear();
            var employees = logic.GetEmployees();
            foreach (var employee in employees)
            {
                listBoxEmployees.Items.Add(employee);
            }
        }

        private void listBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                txtIndex.Text = listBoxEmployees.SelectedIndex.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    logic.AddEmployee(form.EmployeeName, form.WorkExperience, form.Vacancy);
                    RefreshEmployeeList();
                    MessageBox.Show("Сотрудник добавлен!");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                var selectedEmployee = logic.GetEmployeeByIndex(listBoxEmployees.SelectedIndex);
                using (var form = new AddForm())
                {
                    form.SetEmployeeData(selectedEmployee.Name, selectedEmployee.WorkExp, selectedEmployee.Vacancy);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        bool success = logic.UpdateEmployee(
                            listBoxEmployees.SelectedIndex,
                            form.EmployeeName,
                            form.Vacancy,
                            form.WorkExperience
                        );

                        if (success)
                        {
                            RefreshEmployeeList();
                            MessageBox.Show("Сотрудник обновлен!");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении сотрудника!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для изменения");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого сотрудника?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo
                );

                if (result == DialogResult.Yes)
                {
                    logic.DeleteEmployee(listBoxEmployees.SelectedIndex);
                    RefreshEmployeeList();
                    MessageBox.Show("Сотрудник удален!");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления");
            }
        }

        private void btnCalculateSalary_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                try
                {
                    var employee = logic.GetEmployeeByIndex(listBoxEmployees.SelectedIndex);
                    double salary = logic.CalculateSalary(employee);
                    MessageBox.Show($"Зарплата сотрудника {employee.Name}: {salary:C}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для расчета зарплаты");
            }
        }

        private void btnAddExperience_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedIndex >= 0)
            {
                try
                {
                    var employee = logic.GetEmployeeByIndex(listBoxEmployees.SelectedIndex);
                    int oldExp = employee.WorkExp;
                    logic.AddWorkExp(employee);
                    RefreshEmployeeList();
                    MessageBox.Show($"Сотруднику {employee.Name} добавлен 1 год стажа!\nБыло: {oldExp} лет\nСтало: {employee.WorkExp} лет");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для добавления стажа");
            }
        }

        private void btnFindByIndex_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIndex.Text, out int index))
            {
                try
                {
                    var employee = logic.GetEmployeeByIndex(index);
                    MessageBox.Show($"Найден сотрудник: {employee}", "Результат поиска");

                    if (index >= 0 && index < listBoxEmployees.Items.Count)
                    {
                        listBoxEmployees.SelectedIndex = index;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Введите корректный индекс", "Ошибка");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshEmployeeList();
        }

        private void lblIndex_Click(object sender, EventArgs e)
        {
        }

        private void txtIndex_TextChanged(object sender, EventArgs e)
        {
        }
    }

    public class AddForm : Form
    {
        private TextBox txtName;
        private NumericUpDown numericWorkExp;
        private ComboBox comboBoxVacancy;
        private Button btnOK;
        private Button btnCancel;
        private Label lblName;
        private Label lblWorkExp;
        private Label lblVacancy;

        public string EmployeeName { get; private set; }
        public int WorkExperience { get; private set; }
        public VacancyType Vacancy { get; private set; }

        public AddForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.numericWorkExp = new NumericUpDown();
            this.comboBoxVacancy = new ComboBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.lblName = new Label();
            this.lblWorkExp = new Label();
            this.lblVacancy = new Label();

            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Size = new System.Drawing.Size(100, 20);
            this.lblName.Text = "Имя:";

            this.lblWorkExp.Location = new System.Drawing.Point(12, 45);
            this.lblWorkExp.Size = new System.Drawing.Size(100, 20);
            this.lblWorkExp.Text = "Опыт работы:";

            this.lblVacancy.Location = new System.Drawing.Point(12, 75);
            this.lblVacancy.Size = new System.Drawing.Size(100, 20);
            this.lblVacancy.Text = "Должность:";

            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Size = new System.Drawing.Size(200, 20);

            this.numericWorkExp.Location = new System.Drawing.Point(100, 42);
            this.numericWorkExp.Size = new System.Drawing.Size(100, 20);
            this.numericWorkExp.Minimum = 0;
            this.numericWorkExp.Maximum = 50;

            this.comboBoxVacancy.Location = new System.Drawing.Point(100, 72);
            this.comboBoxVacancy.Size = new System.Drawing.Size(200, 20);
            this.comboBoxVacancy.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnOK.Location = new System.Drawing.Point(100, 110);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Text = "OK";
            this.btnOK.Click += new EventHandler(btnOK_Click);

            this.btnCancel.Location = new System.Drawing.Point(185, 110);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            comboBoxVacancy.Items.Add(VacancyType.Head);
            comboBoxVacancy.Items.Add(VacancyType.Manager);
            comboBoxVacancy.Items.Add(VacancyType.Intern);

            if (comboBoxVacancy.Items.Count > 0)
                comboBoxVacancy.SelectedIndex = 0;

            this.Controls.Add(lblName);
            this.Controls.Add(lblWorkExp);
            this.Controls.Add(lblVacancy);
            this.Controls.Add(txtName);
            this.Controls.Add(numericWorkExp);
            this.Controls.Add(comboBoxVacancy);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.Text = "Добавить сотрудника";
            this.Size = new System.Drawing.Size(320, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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