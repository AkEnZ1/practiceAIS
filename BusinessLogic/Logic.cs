using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// Фасад бизнес-логики приложения, реализующий паттерн Фасад.
    /// Предоставляет единый упрощенный интерфейс для работы со всеми
    /// бизнес-сервисами приложения.
    /// </summary>
    /// <remarks>
    /// Объединяет функциональность нескольких сервисов в единый интерфейс,
    /// упрощая взаимодействие с бизнес-логикой для презентеров.
    /// Инкапсулирует сложность взаимодействия между сервисами и
    /// обеспечивает согласованность операций.
    /// </remarks>
    internal class Logic : ILogic
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        /// <summary>
        /// Инициализирует новый экземпляр Logic с указанным репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным сотрудников</param>
        /// <remarks>
        /// Создает экземпляры всех необходимых сервисов бизнес-логики,
        /// инкапсулируя их создание внутри фасада.
        /// </remarks>
        public Logic(IRepository<Employee> repository)
        {
            _employeeService = new EmployeeService(repository);
            _salaryCalculator = new SalaryCalculator();
            _statisticsService = new StatisticsService(repository);
        }

        /// <summary>
        /// Получает сервис управления сотрудниками
        /// </summary>
        public IEmployeeService EmployeeService => _employeeService;

        /// <summary>
        /// Получает калькулятор зарплат
        /// </summary>
        public ISalaryCalculator SalaryCalculator => _salaryCalculator;

        /// <summary>
        /// Получает сервис статистики
        /// </summary>
        public IStatisticsService StatisticsService => _statisticsService;

        // Employee operations
        /// <inheritdoc/>
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
            => _employeeService.AddEmployee(name, workExp, vacancy);

        /// <inheritdoc/>
        public List<Employee> GetEmployees()
            => _employeeService.GetEmployees();

        /// <inheritdoc/>
        public Employee GetEmployeeByIndex(int index)
            => _employeeService.GetEmployeeByIndex(index);

        /// <inheritdoc/>
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
            => _employeeService.UpdateEmployee(index, name, vacancy, workExp);

        /// <inheritdoc/>
        public void DeleteEmployee(int index)
            => _employeeService.DeleteEmployee(index);

        /// <inheritdoc/>
        public List<Employee> GetEmployeesByVacancy(VacancyType vacancy)
            => _employeeService.GetEmployeesByVacancy(vacancy);

        /// <inheritdoc/>
        public void AddWorkExp(Employee employee)
            => _employeeService.AddWorkExp(employee);

        // Salary operations
        /// <inheritdoc/>
        public double CalculateSalary(Employee employee)
            => _salaryCalculator.CalculateSalary(employee);

        // Statistics operations
        /// <inheritdoc/>
        public int GetTotalEmployees()
            => _statisticsService.GetTotalEmployees();

        /// <inheritdoc/>
        public double GetAverageExperience()
            => _statisticsService.GetAverageExperience();

        /// <inheritdoc/>
        public Dictionary<VacancyType, int> GetVacancyDistribution()
            => _statisticsService.GetVacancyDistribution();

        /// <inheritdoc/>
        public Employee GetMostExperiencedEmployee()
            => _statisticsService.GetMostExperiencedEmployee();

        /// <inheritdoc/>
        public double GetTotalSalaryBudget()
        {
            var employees = GetEmployees();
            double total = 0;
            foreach (var employee in employees)
            {
                total += CalculateSalary(employee);
            }
            return total;
        }
    }
}