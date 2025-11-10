using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// Фасад бизнес-логики приложения
    /// </summary>
    /// <remarks>
    /// Координирует работу различных сервисов и предоставляет упрощенный интерфейс для клиентов.
    /// Реализует принцип инверсии зависимостей (DIP) через внедрение зависимостей в конструкторе.
    /// Выступает единой точкой входа для всех операций с сотрудниками.
    /// </remarks>
    public class Logic
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        /// <summary>
        /// Инициализирует новый экземпляр Logic с указанным репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными сотрудников</param>
        public Logic(IRepository<Employee> repository)
        {
            _employeeService = new EmployeeService(repository);
            _salaryCalculator = new SalaryCalculator();
            _statisticsService = new StatisticsService(repository);
        }

        // === МЕТОДЫ ДЛЯ РАБОТЫ С СОТРУДНИКАМИ ===

        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="workExp">Опыт работы в годах</param>
        /// <param name="vacancy">Должность сотрудника</param>
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
            => _employeeService.AddEmployee(name, workExp, vacancy);

        /// <summary>
        /// Получает список всех сотрудников
        /// </summary>
        /// <returns>Список всех сотрудников</returns>
        public List<Employee> GetEmployees()
            => _employeeService.GetEmployees();

        /// <summary>
        /// Получает сотрудника по индексу в списке
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <returns>Найденный сотрудник</returns>
        public Employee GetEmployeeByIndex(int index)
            => _employeeService.GetEmployeeByIndex(index);

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        /// <param name="name">Новое имя</param>
        /// <param name="vacancy">Новая должность</param>
        /// <param name="workExp">Новый опыт работы</param>
        /// <returns>True если обновление успешно</returns>
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
            => _employeeService.UpdateEmployee(index, name, vacancy, workExp);

        /// <summary>
        /// Удаляет сотрудника по индексу
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        public void DeleteEmployee(int index)
            => _employeeService.DeleteEmployee(index);

        /// <summary>
        /// Получает сотрудников по указанной должности
        /// </summary>
        /// <param name="vacancy">Тип должности</param>
        /// <returns>Список сотрудников с указанной должностью</returns>
        public List<Employee> GetEmployeesByVacancy(VacancyType vacancy)
            => _employeeService.GetEmployeesByVacancy(vacancy);

        /// <summary>
        /// Добавляет один год стажа сотруднику
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void AddWorkExp(Employee employee)
            => _employeeService.AddWorkExp(employee);

        // === МЕТОДЫ ДЛЯ РАСЧЕТА ЗАРПЛАТ ===

        /// <summary>
        /// Рассчитывает зарплату сотрудника на основе должности и опыта работы
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Рассчитанная зарплата</returns>
        public double CalculateSalary(Employee employee)
            => _salaryCalculator.CalculateSalary(employee);

        // === НОВЫЕ МЕТОДЫ ДЛЯ СТАТИСТИКИ И ОТЧЕТНОСТИ ===

        /// <summary>
        /// Получает общее количество сотрудников в системе
        /// </summary>
        /// <returns>Количество сотрудников</returns>
        public int GetTotalEmployees()
            => _statisticsService.GetTotalEmployees();

        /// <summary>
        /// Рассчитывает средний опыт работы всех сотрудников
        /// </summary>
        /// <returns>Средний опыт работы в годах</returns>
        public double GetAverageExperience()
            => _statisticsService.GetAverageExperience();

        /// <summary>
        /// Получает распределение сотрудников по должностям
        /// </summary>
        /// <returns>Словарь где ключ - должность, значение - количество сотрудников</returns>
        public Dictionary<VacancyType, int> GetVacancyDistribution()
            => _statisticsService.GetVacancyDistribution();

        /// <summary>
        /// Находит сотрудника с наибольшим опытом работы
        /// </summary>
        /// <returns>Сотрудник с максимальным опытом работы или null если сотрудников нет</returns>
        public Employee GetMostExperiencedEmployee()
            => _statisticsService.GetMostExperiencedEmployee();

        /// <summary>
        /// Рассчитывает общую сумму зарплат всех сотрудников
        /// </summary>
        /// <returns>Общий фонд заработной платы</returns>
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