using DataAccessLayer;
using LogicAndModel;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Logic logic;

        public Form1()
        {
            InitializeComponent();

            InitializeDatabase();

            IRepository<Employee> repository = new DapperRepository();
            logic = new Logic(repository);

            RefreshEmployeeList();
        }

        private void InitializeDatabase()
        {
            try
            {
                // Создаем файл БД если не существует
                if (!System.IO.File.Exists("EmployeeDatabase.sqlite"))
                {
                    SQLiteConnection.CreateFile("EmployeeDatabase.sqlite");
                }

                // Создаем таблицу через чистый SQLite
                using (var connection = new SQLiteConnection("Data Source=EmployeeDatabase.sqlite"))
                {
                    connection.Open();

                    // Создаем таблицу
                    using (var command = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Employees (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Vacancy INTEGER NOT NULL,
                    WorkExp INTEGER NOT NULL
                )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}");
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCalculateSalary = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddExperience = new System.Windows.Forms.Button();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.btnFindByIndex = new System.Windows.Forms.Button();
            this.lblIndex = new System.Windows.Forms.Label();
            this.btnFilterManagers = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            this.dataGridViewEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridViewEmployees.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewEmployees.MultiSelect = false;
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(640, 200);
            this.dataGridViewEmployees.TabIndex = 12;
            this.dataGridViewEmployees.SelectionChanged += new System.EventHandler(this.dataGridViewEmployees_SelectionChanged);
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
            this.btnRefresh.Location = new System.Drawing.Point(86, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Обновить список";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddExperience
            // 
            this.btnAddExperience.Location = new System.Drawing.Point(224, 259);
            this.btnAddExperience.Name = "btnAddExperience";
            this.btnAddExperience.Size = new System.Drawing.Size(100, 30);
            this.btnAddExperience.TabIndex = 5;
            this.btnAddExperience.Text = "Добавить стаж";
            this.btnAddExperience.Click += new System.EventHandler(this.btnAddExperience_Click);
            // 
            // txtIndex
            // 
            this.txtIndex.Location = new System.Drawing.Point(597, 234);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(50, 20);
            this.txtIndex.TabIndex = 7;
            // 
            // btnFindByIndex
            // 
            this.btnFindByIndex.Location = new System.Drawing.Point(456, 220);
            this.btnFindByIndex.Name = "btnFindByIndex";
            this.btnFindByIndex.Size = new System.Drawing.Size(130, 30);
            this.btnFindByIndex.TabIndex = 8;
            this.btnFindByIndex.Text = "Найти по индексу";
            this.btnFindByIndex.Click += new System.EventHandler(this.btnFindByIndex_Click);
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(599, 218);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(48, 13);
            this.lblIndex.TabIndex = 9;
            this.lblIndex.Text = "Индекс:";
            // 
            // btnFilterManagers
            // 
            this.btnFilterManagers.Location = new System.Drawing.Point(348, 259);
            this.btnFilterManagers.Name = "btnFilterManagers";
            this.btnFilterManagers.Size = new System.Drawing.Size(133, 30);
            this.btnFilterManagers.TabIndex = 10;
            this.btnFilterManagers.Text = "Показать менеджеров";
            this.btnFilterManagers.Click += new System.EventHandler(this.btnFilterManagers_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(487, 260);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(120, 30);
            this.btnShowAll.TabIndex = 11;
            this.btnShowAll.Text = "Показать всех";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Имя";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Должность";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Опыт работы (лет)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(664, 301);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnFilterManagers);
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.btnFindByIndex);
            this.Controls.Add(this.txtIndex);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAddExperience);
            this.Controls.Add(this.btnCalculateSalary);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Name = "Form1";
            this.Text = "Управление сотрудниками";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridView dataGridViewEmployees;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCalculateSalary;
        private Button btnRefresh;
        private Button btnAddExperience;
        private TextBox txtIndex;
        private Button btnFindByIndex;
        private Label lblIndex;
        private Button btnFilterManagers;
        private Button btnShowAll;

        private void RefreshEmployeeList()
        {
            dataGridViewEmployees.Rows.Clear();
            var employees = logic.GetEmployees();

            foreach (var employee in employees)
            {
                dataGridViewEmployees.Rows.Add(
                    employee.ID,
                    employee.Name,
                    GetVacancyRussianName(employee.Vacancy),
                    employee.WorkExp
                );
            }
        }

        private string GetVacancyRussianName(VacancyType vacancy)
        {
            switch (vacancy)
            {
                case VacancyType.Head: return "Руководитель";
                case VacancyType.Manager: return "Менеджер";
                case VacancyType.Intern: return "Стажер";
                default: return "Неизвестно";
            }
        }

        private void dataGridViewEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                txtIndex.Text = dataGridViewEmployees.SelectedRows[0].Index.ToString();
            }
        }

        private int GetSelectedEmployeeIndex()
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                return dataGridViewEmployees.SelectedRows[0].Index;
            }
            return -1;
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
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                var selectedEmployee = logic.GetEmployeeByIndex(selectedIndex);
                using (var form = new AddForm())
                {
                    form.SetEmployeeData(selectedEmployee.Name, selectedEmployee.WorkExp, selectedEmployee.Vacancy);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        bool success = logic.UpdateEmployee(
                            selectedIndex,
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
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого сотрудника?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo
                );

                if (result == DialogResult.Yes)
                {
                    logic.DeleteEmployee(selectedIndex);
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
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                try
                {
                    var employee = logic.GetEmployeeByIndex(selectedIndex);
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
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                try
                {
                    var employee = logic.GetEmployeeByIndex(selectedIndex);
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

                    if (index >= 0 && index < dataGridViewEmployees.Rows.Count)
                    {
                        dataGridViewEmployees.ClearSelection();
                        dataGridViewEmployees.Rows[index].Selected = true;
                        dataGridViewEmployees.FirstDisplayedScrollingRowIndex = index;
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

        private void btnFilterManagers_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEmployees.Rows.Clear();
                var employees = logic.GetEmployees();
                var managers = employees.Where(emp => emp.Vacancy == VacancyType.Manager).ToList();

                foreach (var employee in managers)
                {
                    dataGridViewEmployees.Rows.Add(
                        employee.ID,
                        employee.Name,
                        GetVacancyRussianName(employee.Vacancy),
                        employee.WorkExp
                    );
                }

                MessageBox.Show($"Показаны {managers.Count} менеджера(ов)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}");
            }
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshEmployeeList();
            MessageBox.Show("Показаны все сотрудники");
        }

        private void lblIndex_Click(object sender, EventArgs e) { }
        private void txtIndex_TextChanged(object sender, EventArgs e) { }
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