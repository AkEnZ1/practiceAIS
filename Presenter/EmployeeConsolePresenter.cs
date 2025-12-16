using BusinessLogic.Interfaces;
using ConsoleApp;
using DomainModel;
using System;
using System.Collections.Generic;

namespace Presenter
{
    public class EmployeeConsolePresenter
    {
        private readonly EmployeeConsoleView _view;
        private readonly ILogic _logic;

        public EmployeeConsolePresenter(EmployeeConsoleView view, ILogic logic)
        {
            _view = view;
            _logic = logic;

            // Подписываемся на события View как у одногруппника
            _view.StartupEvent += () => RefreshEmployeeList();
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
        }

        private void HandleAddEmployee(string name, int workExp, VacancyType vacancy)
        {
            try
            {
                _logic.AddEmployee(name, workExp, vacancy);
                RefreshEmployeeList();
                _view.ShowMessage("Сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
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
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
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
                _view.ShowError($"Ошибка: {ex.Message}");
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
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void HandleCalculateSalary(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                var salary = _logic.CalculateSalary(employee);
                _view.ShowMessage($"Зарплата: {salary:F2} руб.");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void HandleAddWorkExperience(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _logic.AddWorkExp(employee);
                RefreshEmployeeList();
                _view.ShowMessage("Стаж добавлен!");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void HandleShowStatistics()
        {
            try
            {
                var total = _logic.GetTotalEmployees();
                var avgExp = _logic.GetAverageExperience();
                var totalBudget = _logic.GetTotalSalaryBudget();
                var mostExp = _logic.GetMostExperiencedEmployee();

                var stats = $"Всего сотрудников: {total}\n" +
                           $"Средний стаж: {avgExp:F1} лет\n" +
                           $"Общий бюджет: {totalBudget:F2} руб.\n";

                if (mostExp != null)
                {
                    stats += $"Самый опытный: {mostExp.Name} ({mostExp.WorkExp} лет)";
                }

                _view.ShowMessage(stats);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void HandleFilterByVacancy(VacancyType vacancy)
        {
            try
            {
                var employees = _logic.GetEmployeesByVacancy(vacancy);
                _view.RefreshEmployeeList(employees);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void HandleShowAllEmployees()
        {
            RefreshEmployeeList();
        }

        private void HandleFindByIndex(int index)
        {
            try
            {
                var employee = _logic.GetEmployeeByIndex(index);
                _view.ShowMessage($"Найден: {employee}");
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void RefreshEmployeeList()
        {
            var employees = _logic.GetEmployees();
            _view.RefreshEmployeeList(employees);
        }
    }
}