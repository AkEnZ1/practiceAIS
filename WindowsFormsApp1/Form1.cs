using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DomainModel;
using Shared.Interfaces;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form, IEmployeeView
    {

        public Form1()
        {
            InitializeComponent();
            ApplyStyling();
        }

        #region Реализация IEmployeeView

        public void RefreshEmployeeList(List<Employee> employees)
        {
            if (dataGridViewEmployees.InvokeRequired)
            {
                dataGridViewEmployees.Invoke(new Action<List<Employee>>(RefreshEmployeeList), employees);
                return;
            }

            dataGridViewEmployees.Rows.Clear();
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

        public void ShowEmployeeDetails(Employee employee)
        {
            ShowMessage($"Детали сотрудника:\n{employee}");
        }

        public void ClearEmployeeDetails()
        {
            // Реализация при наличии отдельного контрола для деталей
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region События IEmployeeView

        public event Action<string, int, VacancyType> OnAddEmployee;
        public event Action<int, string, VacancyType, int> OnUpdateEmployee;
        public event Action<int> OnDeleteEmployee;
        public event Action<int> OnEmployeeSelected;
        public event Action<int> OnCalculateSalary;
        public event Action<int> OnAddWorkExperience;
        public event Action OnShowStatistics;
        public event Action<VacancyType> OnFilterByVacancy;
        public event Action OnShowAllEmployees;
        public event Action<int> OnFindByIndex;

        #endregion

        #region Обработчики событий формы

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    OnAddEmployee?.Invoke(form.EmployeeName, form.WorkExperience, form.Vacancy);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                var selectedEmployee = GetEmployeeFromGrid(selectedIndex);
                using (var form = new AddForm())
                {
                    form.SetEmployeeData(selectedEmployee.Name, selectedEmployee.WorkExp, selectedEmployee.Vacancy);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        OnUpdateEmployee?.Invoke(selectedIndex, form.EmployeeName, form.Vacancy, form.WorkExperience);
                    }
                }
            }
            else
            {
                ShowError("Выберите сотрудника для изменения");
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
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    OnDeleteEmployee?.Invoke(selectedIndex);
                }
            }
            else
            {
                ShowError("Выберите сотрудника для удаления");
            }
        }

        private void btnCalculateSalary_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                OnCalculateSalary?.Invoke(selectedIndex);
            }
            else
            {
                ShowError("Выберите сотрудника для расчета зарплаты");
            }
        }

        private void btnAddExperience_Click(object sender, EventArgs e)
        {
            int selectedIndex = GetSelectedEmployeeIndex();
            if (selectedIndex >= 0)
            {
                OnAddWorkExperience?.Invoke(selectedIndex);
            }
            else
            {
                ShowError("Выберите сотрудника для добавления стажа");
            }
        }

        private void btnStatistics_Click_1(object sender, EventArgs e)
        {
            OnShowStatistics?.Invoke();
        }

        private void btnFindByIndex_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIndex.Text, out int index))
            {
                OnFindByIndex?.Invoke(index);

                // Подсветка найденной строки
                if (index >= 0 && index < dataGridViewEmployees.Rows.Count)
                {
                    dataGridViewEmployees.ClearSelection();
                    dataGridViewEmployees.Rows[index].Selected = true;
                    dataGridViewEmployees.FirstDisplayedScrollingRowIndex = index;
                }
            }
            else
            {
                ShowError("Введите корректный индекс");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnShowAllEmployees?.Invoke();
        }

        private void btnFilterManagers_Click(object sender, EventArgs e)
        {
            OnFilterByVacancy?.Invoke(VacancyType.Manager);
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            OnShowAllEmployees?.Invoke();
        }

        private void dataGridViewEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                int index = dataGridViewEmployees.SelectedRows[0].Index;
                txtIndex.Text = index.ToString();
            }
        }

        #endregion

        #region Вспомогательные методы

        private int GetSelectedEmployeeIndex()
        {
            return dataGridViewEmployees.SelectedRows.Count > 0
                ? dataGridViewEmployees.SelectedRows[0].Index
                : -1;
        }

        private Employee GetEmployeeFromGrid(int index)
        {
            if (index >= 0 && index < dataGridViewEmployees.Rows.Count)
            {
                var row = dataGridViewEmployees.Rows[index];
                return new Employee
                {
                    ID = (int)row.Cells[0].Value,
                    Name = row.Cells[1].Value?.ToString() ?? "",
                    Vacancy = GetVacancyFromRussianName(row.Cells[2].Value?.ToString() ?? ""),
                    WorkExp = (int)row.Cells[3].Value
                };
            }
            return null;
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

        private VacancyType GetVacancyFromRussianName(string name)
        {
            switch (name)
            {
                case "Руководитель": return VacancyType.Head;
                case "Менеджер": return VacancyType.Manager;
                case "Стажер": return VacancyType.Intern;
                default: return VacancyType.Intern;
            }
        }

        private void ApplyStyling()
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
            dataGridViewEmployees.BackgroundColor = Color.White;
            dataGridViewEmployees.BorderStyle = BorderStyle.None;
            dataGridViewEmployees.EnableHeadersVisualStyles = false;
            dataGridViewEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 97, 97);
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

        #endregion

        // Пустые обработчики для дизайнера
        private void lblIndex_Click(object sender, EventArgs e) { }
        private void txtIndex_TextChanged(object sender, EventArgs e) { }
    }
}
