using BusinessLogic.Interfaces;
using DomainModel;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Presenter
{
    public class EmployeeWinFormsPresenter
    {
        private readonly IEmployeeView _view;
        private readonly ILogic _logic;

        public EmployeeWinFormsPresenter(IEmployeeView view, ILogic logic)
        {
            _view = view;
            _logic = logic;

            // Подписываемся на события View
            _view.OnAddEmployee += HandleAddEmployee;
            _view.OnUpdateEmployee += HandleUpdateEmployee;
            _view.OnDeleteEmployee += HandleDeleteEmployee;
            _view.OnEmployeeSelected += HandleEmployeeSelected;
            _view.OnCalculateSalary += HandleCalculateSalary;
            _view.OnAddWorkExperience += HandleAddWorkExperience;
            _view.OnShowStatistics += HandleShowStatistics;
            _view.OnFilterByVacancy += HandleFilterByVacancy;
            _view.OnShowAllEmployees += HandleShowAllEmployees;
            _view.OnFindByIndex += HandleFindByIndex;

            // Инициализация - загружаем сотрудников
            RefreshEmployeeList();
        }

        private void HandleAddEmployee(string name, int workExp, VacancyType vacancy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _view.ShowError("Имя не может быть пустым!");
                    return;
                }

                if (workExp < 0)
                {
                    _view.ShowError("Опыт работы должен быть неотрицательным числом!");
                    return;
                }

                _logic.AddEmployee(name, workExp, vacancy);
                RefreshEmployeeList();
                _view.ShowMessage("Сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при добавлении сотрудника: {ex.Message}");
            }
        }

        private void HandleUpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            try
            {
                var success = _logic.UpdateEmployee(index, name, vacancy, workExp);
                if (success)
                {
                    RefreshEmployeeList();
                    _view.ShowMessage("Сотрудник обновлен!");
                }
                else
                {
                    _view.ShowError("Неверный индекс сотрудника!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при обновлении сотрудника: {ex.Message}");
            }
        }

        private void HandleDeleteEmployee(int index)
        {
            try
            {
                _logic.DeleteEmployee(index);
                RefreshEmployeeList();
                _view.ShowMessage("Сотрудник удален!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при удалении сотрудника: {ex.Message}");
            }
        }

        private void HandleEmployeeSelected(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _view.ShowEmployeeDetails(employee);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при выборе сотрудника: {ex.Message}");
            }
        }

        private void HandleCalculateSalary(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                var salary = _logic.CalculateSalary(employee);
                _view.ShowMessage($"Зарплата сотрудника {employee.Name}: {salary:F2} руб.");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при расчете зарплаты: {ex.Message}");
            }
        }

        private void HandleAddWorkExperience(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                int oldExp = employee.WorkExp;
                _logic.AddWorkExp(employee);
                RefreshEmployeeList();
                _view.ShowMessage($"Сотруднику {employee.Name} добавлен 1 год стажа!\nБыло: {oldExp} лет\nСтало: {employee.WorkExp} лет");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при добавлении стажа: {ex.Message}");
            }
        }

        private void HandleShowStatistics()
        {
            try
            {
                var totalEmployees = _logic.GetTotalEmployees();
                var avgExperience = _logic.GetAverageExperience();
                var distribution = _logic.GetVacancyDistribution();
                var mostExperienced = _logic.GetMostExperiencedEmployee();
                var totalBudget = _logic.GetTotalSalaryBudget();

                var statistics = $"=== СТАТИСТИКА ПО СОТРУДНИКАМ ===\n\n" +
                               $"Общее количество сотрудников: {totalEmployees}\n" +
                               $"Средний опыт работы: {avgExperience:F1} лет\n" +
                               $"Общий бюджет на зарплаты: {totalBudget:F2} руб.\n\n" +
                               $"Распределение по должностям:\n";

                foreach (var item in distribution)
                {
                    statistics += $"  {GetVacancyRussianName(item.Key)}: {item.Value} чел.\n";
                }

                if (mostExperienced != null)
                {
                    var salary = _logic.CalculateSalary(mostExperienced);
                    statistics += $"\nСамый опытный сотрудник: {mostExperienced.Name}\n" +
                                 $"  Должность: {GetVacancyRussianName(mostExperienced.Vacancy)}\n" +
                                 $"  Опыт работы: {mostExperienced.WorkExp} лет\n" +
                                 $"  Зарплата: {salary:F2} руб.";
                }

                _view.ShowMessage(statistics);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при получении статистики: {ex.Message}");
            }
        }

        private void HandleFilterByVacancy(VacancyType vacancy)
        {
            try
            {
                var employees = _logic.GetEmployeesByVacancy(vacancy);
                _view.RefreshEmployeeList(employees);
                _view.ShowMessage($"Показаны {employees.Count} {GetVacancyRussianName(vacancy).ToLower()}(ов)");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при фильтрации: {ex.Message}");
            }
        }

        private void HandleShowAllEmployees()
        {
            RefreshEmployeeList();
            _view.ShowMessage("Показаны все сотрудники");
        }

        private void HandleFindByIndex(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _view.ShowMessage($"Найден сотрудник: {employee}");
                _view.ShowEmployeeDetails(employee);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при поиске: {ex.Message}");
            }
        }

        private void RefreshEmployeeList()
        {
            try
            {
                var employees = _logic.GetEmployees();
                _view.RefreshEmployeeList(employees);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
        }

        private string GetVacancyRussianName(VacancyType vacancy)
        {
            switch (vacancy)
            {
                case VacancyType.Head:
                    return "Руководитель";
                case VacancyType.Manager:
                    return "Менеджер";
                case VacancyType.Intern:
                    return "Стажер";
                default:
                    return "Неизвестно";
            }
        }
    }
}