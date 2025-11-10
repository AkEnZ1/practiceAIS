using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с сотрудниками
    /// </summary>
    /// <remarks>
    /// Определяет контракт для операций управления сотрудниками.
    /// Реализует принцип разделения интерфейсов (ISP).
    /// </remarks>
    public interface IEmployeeService
    {
        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="workExp">Опыт работы в годах</param>
        /// <param name="vacancy">Должность сотрудника</param>
        void AddEmployee(string name, int workExp, VacancyType vacancy);

        /// <summary>
        /// Получает список всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        List<Employee> GetEmployees();

        /// <summary>
        /// Получает сотрудника по индексу в списке
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        /// <returns>Найденный сотрудник</returns>
        Employee GetEmployeeByIndex(int index);

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника</param>
        /// <param name="name">Новое имя</param>
        /// <param name="vacancy">Новая должность</param>
        /// <param name="workExp">Новый опыт работы</param>
        /// <returns>True если обновление успешно</returns>
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
        /// <param name="employee">Сотрудник</param>
        void AddWorkExp(Employee employee);
    }
}