using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DomainModel;
using Shared.Interfaces;
using Microsoft.VisualBasic; // Для InputBox

namespace WindowsFormsApp1
{
    public partial class Form1 : Form, IEmployeeView
    {
        // Событие StartupEvent как у одногруппника
        public event Action StartupEvent;

        // События IEmployeeView
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

        public Form1()
        {
            InitializeComponent();
            ApplyStyling();

            // Подписываемся на событие загрузки формы
            this.Load += Form1_Load;
        }

        // Метод Start() можно оставить пустым или удалить
        public void Start()
        {
            // При загрузке формы вызываем StartupEvent
            StartupEvent?.Invoke();
        }

        // Вызов StartupEvent при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            StartupEvent?.Invoke();
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

        // Реализация методов чтения данных для совместимости с интерфейсом
        public string ReadString(string prompt)
        {
            // Создаем простейшую форму для ввода
            using (var form = new Form())
            {
                form.Text = "Ввод";
                form.Size = new Size(300, 120);
                form.StartPosition = FormStartPosition.CenterScreen;

                var textBox = new TextBox
                {
                    Location = new Point(10, 30),
                    Size = new Size(260, 20)
                };

                var button = new Button
                {
                    Text = "OK",
                    Location = new Point(100, 60),
                    DialogResult = DialogResult.OK
                };

                form.Controls.Add(new Label
                {
                    Text = prompt,
                    Location = new Point(10, 10)
                });
                form.Controls.Add(textBox);
                form.Controls.Add(button);
                form.AcceptButton = button;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        public int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                string input = ReadString($"{prompt} (от {min} до {max}):");
                if (int.TryParse(input, out int result) && result >= min && result <= max)
                {
                    return result;
                }
                ShowError($"Введите число от {min} до {max}");
            }
        }

        public VacancyType ReadVacancy(string prompt)
        {
            // Создаем простую форму для выбора должности
            using (var selectionForm = new Form())
            {
                selectionForm.Text = prompt;
                selectionForm.Size = new Size(300, 150);
                selectionForm.StartPosition = FormStartPosition.CenterParent;
                selectionForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectionForm.MaximizeBox = false;
                selectionForm.MinimizeBox = false;

                var label = new Label
                {
                    Text = "Выберите должность:",
                    Location = new Point(10, 20),
                    Size = new Size(280, 20)
                };

                var comboBox = new ComboBox
                {
                    Location = new Point(10, 50),
                    Size = new Size(260, 21),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                comboBox.Items.AddRange(new object[] { "Руководитель", "Менеджер", "Стажер" });
                comboBox.SelectedIndex = 0;

                var okButton = new Button
                {
                    Text = "OK",
                    Location = new Point(100, 80),
                    Size = new Size(75, 23),
                    DialogResult = DialogResult.OK
                };

                selectionForm.Controls.Add(label);
                selectionForm.Controls.Add(comboBox);
                selectionForm.Controls.Add(okButton);
                selectionForm.AcceptButton = okButton;

                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    switch (comboBox.SelectedItem.ToString())
                    {
                        case "Руководитель":
                            return VacancyType.Head;
                        case "Менеджер":
                            return VacancyType.Manager;
                        case "Стажер":
                            return VacancyType.Intern;
                    }
                }
            }
            return VacancyType.Intern; // Значение по умолчанию
        }

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