using BusinessLogic.Interfaces;
using BusinessLogic.Logging;
using DomainModel;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Validators;


namespace Presenters {
    /// <summary>
    /// Presenter для управления сотрудниками - View передается через AttachView
    /// </summary>
    public class EmployeePresenter
    {
        private IEmployeeView _view;
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        /// <summary>
        /// Конструктор Presenter (View не передается)
        /// </summary>
        public EmployeePresenter(
            IEmployeeService employeeService,
            ISalaryCalculator salaryCalculator,
            IStatisticsService statisticsService)
        {
            _employeeService = employeeService;
            _salaryCalculator = salaryCalculator;
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Присоединяет View к Presenter (вызывается из ApplicationController)
        /// </summary>
        public void AttachView(IEmployeeView view)
        {
            if (_view != null)
                throw new InvalidOperationException("View уже присоединен к Presenter");

            _view = view;
            SubscribeToViewEvents();

        }

        /// <summary>
        /// Отсоединяет View от Presenter
        /// </summary>
        public void DetachView()
        {
            if (_view == null) return;

            UnsubscribeFromViewEvents();
            _view = null;
        }

        private void SubscribeToViewEvents()
        {
            if (_view == null) return;

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

        private void UnsubscribeFromViewEvents()
        {
            if (_view == null) return;

            _view.OnAddEmployee -= HandleAddEmployee;
            _view.OnUpdateEmployee -= HandleUpdateEmployee;
            _view.OnDeleteEmployee -= HandleDeleteEmployee;
            _view.OnEmployeeSelected -= HandleEmployeeSelected;
            _view.OnCalculateSalary -= HandleCalculateSalary;
            _view.OnAddWorkExperience -= HandleAddWorkExperience;
            _view.OnShowStatistics -= HandleShowStatistics;
            _view.OnFilterByVacancy -= HandleFilterByVacancy;
            _view.OnShowAllEmployees -= HandleShowAllEmployees;
            _view.OnFindByIndex -= HandleFindByIndex;
        }


        private void RefreshEmployeeList()
        {
            try
            {
                var employees = _employeeService.GetEmployees();
                _view.RefreshEmployeeList(employees);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
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
                string logMessage = $"{DateTime.Now}: Добавляем сотрудника {name}\n";
                System.IO.File.AppendAllText("log.txt", logMessage);
                _employeeService.AddEmployee(name, workExp, vacancy);
                RefreshEmployeeList();
                string successLog = $"{DateTime.Now}: Сотрудник {name} добавлен успешно\n";
                System.IO.File.AppendAllText("log.txt", successLog);

                _view.ShowMessage("Сотрудник добавлен!");
            }
            catch (Exception ex)
            {
                string errorLog = $"{DateTime.Now}: ОШИБКА при добавлении {name} - {ex.Message}\n";
                System.IO.File.AppendAllText("log.txt", errorLog);

                _view.ShowError($"Ошибка при добавлении сотрудника: {ex.Message}");
            }
        }

        private void HandleUpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            try
            {
                var success = _employeeService.UpdateEmployee(index, name, vacancy, workExp);
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
                _employeeService.DeleteEmployee(index);
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
                var employee = _employeeService.GetEmployeeByIndex(index);
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
                var employee = _employeeService.GetEmployeeByIndex(index);
                var salary = _salaryCalculator.CalculateSalary(employee);
                _view.ShowMessage($"Зарплата сотрудника {employee.Name}: {salary:C}");
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
                var employee = _employeeService.GetEmployeeByIndex(index);
                int oldExp = employee.WorkExp;
                _employeeService.AddWorkExp(employee);
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
                var totalEmployees = _statisticsService.GetTotalEmployees();
                var avgExperience = _statisticsService.GetAverageExperience();
                var distribution = _statisticsService.GetVacancyDistribution();
                var mostExperienced = _statisticsService.GetMostExperiencedEmployee();

                var statistics = $"=== СТАТИСТИКА ПО СОТРУДНИКАМ ===\n\n" +
                               $"Общее количество сотрудников: {totalEmployees}\n" +
                               $"Средний опыт работы: {avgExperience:F1} лет\n\n" +
                               $"Распределение по должностям:\n";

                foreach (var item in distribution)
                {
                    statistics += $"  {GetVacancyRussianName(item.Key)}: {item.Value} чел.\n";
                }

                if (mostExperienced != null)
                {
                    var salary = _salaryCalculator.CalculateSalary(mostExperienced);
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
                var employees = _employeeService.GetEmployeesByVacancy(vacancy);
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
                var employee = _employeeService.GetEmployeeByIndex(index);
                _view.ShowMessage($"Найден сотрудник: {employee}");
                _view.ShowEmployeeDetails(employee);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при поиске: {ex.Message}");
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
    }
}