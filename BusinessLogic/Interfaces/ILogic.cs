using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс фасада бизнес-логики приложения
    /// </summary>
    /// <remarks>
    /// Определяет контракт для всех операций управления сотрудниками, расчета зарплат и статистической отчетности.
    /// Реализует принцип инверсии зависимостей (DIP) - клиенты зависят от абстракции, а не конкретной реализации.
    /// Обеспечивает единую точку входа для всех бизнес-операций с сотрудниками.
    /// Позволяет легко подменять реализации бизнес-логики для тестирования или расширения функциональности.
    /// </remarks>
    public interface ILogic
    {
        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="workExp">Опыт работы в годах</param>
        /// <param name="vacancy">Должность сотрудника</param>
        void AddEmployee(string name, int workExp, VacancyType vacancy);

        /// <summary>
        /// Получает список всех сотрудников системы
        /// </summary>
        /// <returns>Список всех сотрудников</returns>
        List<Employee> GetEmployees();

        /// <summary>
        /// Получает сотрудника по индексу в списке
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <returns>Найденный сотрудник</returns>
        Employee GetEmployeeByIndex(int index);

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        /// <param name="name">Новое имя</param>
        /// <param name="vacancy">Новая должность</param>
        /// <param name="workExp">Новый опыт работы</param>
        /// <returns>True если обновление успешно, иначе False</returns>
        bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp);

        /// <summary>
        /// Удаляет сотрудника по индексу
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        void DeleteEmployee(int index);

        /// <summary>
        /// Получает сотрудников по указанной должности
        /// </summary>
        /// <param name="vacancy">Тип должности</param>
        /// <returns>Список сотрудников с указанной должностью</returns>
        List<Employee> GetEmployeesByVacancy(VacancyType vacancy);

        /// <summary>
        /// Добавляет один год стажа сотруднику
        /// </summary>
        /// <param name="employee">Сотрудник для добавления стажа</param>
        void AddWorkExp(Employee employee);

        /// <summary>
        /// Рассчитывает зарплату сотрудника на основе должности и опыта работы
        /// </summary>
        /// <param name="employee">Сотрудник для расчета зарплаты</param>
        /// <returns>Рассчитанная зарплата</returns>
        double CalculateSalary(Employee employee);

        /// <summary>
        /// Получает общее количество сотрудников в системе
        /// </summary>
        /// <returns>Количество сотрудников</returns>
        int GetTotalEmployees();

        /// <summary>
        /// Рассчитывает средний опыт работы всех сотрудников
        /// </summary>
        /// <returns>Средний опыт работы в годах</returns>
        double GetAverageExperience();

        /// <summary>
        /// Получает распределение сотрудников по должностям
        /// </summary>
        /// <returns>Словарь где ключ - должность, значение - количество сотрудников</returns>
        Dictionary<VacancyType, int> GetVacancyDistribution();

        /// <summary>
        /// Находит сотрудника с наибольшим опытом работы
        /// </summary>
        /// <returns>Сотрудник с максимальным опытом работы или null если сотрудников нет</returns>
        Employee GetMostExperiencedEmployee();

        /// <summary>
        /// Рассчитывает общую сумму зарплат всех сотрудников
        /// </summary>
        /// <returns>Общий фонд заработной платы</returns>
        double GetTotalSalaryBudget();
    }
}
