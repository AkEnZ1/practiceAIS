using BusinessLogic;
using DataAccessLayer;
using DomainModel;
using Ninject;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Logic logic;

        public Form1()
        {
            InitializeComponent(); // Этот метод находится в Form1.Designer.cs

            InitializeDatabase();

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            logic = ninjectKernel.Get<Logic>();

            RefreshEmployeeList();
            ApplyStyling();
        }
        private void ApplyStyling()
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewEmployees.BackgroundColor = Color.White;
            dataGridViewEmployees.BorderStyle = BorderStyle.None;
            dataGridViewEmployees.EnableHeadersVisualStyles = false;
            dataGridViewEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 97, 97); // Серый
            dataGridViewEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewEmployees.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(117, 117, 117);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                    button.Cursor = Cursors.Hand;
                    button.MouseEnter += (s, e) => { button.BackColor = Color.FromArgb(158, 158, 158); };
                    button.MouseLeave += (s, e) => { button.BackColor = Color.FromArgb(117, 117, 117); };
                }
            }
        }


        private void InitializeDatabase()
        {
            try
            {
                if (!System.IO.File.Exists("EmployeeDatabase.sqlite"))
                {
                    SQLiteConnection.CreateFile("EmployeeDatabase.sqlite");
                }

                using (var connection = new SQLiteConnection("Data Source=EmployeeDatabase.sqlite"))
                {
                    connection.Open();

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
}