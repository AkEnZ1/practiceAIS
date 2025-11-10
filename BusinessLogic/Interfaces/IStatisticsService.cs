using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для получения статистики по сотрудникам
    /// </summary>
    /// <remarks>
    /// Определяет контракт для операций бизнес-аналитики и отчетности.
    /// Реализует принцип разделения интерфейсов (ISP), выделяя статистические операции в отдельный контракт.
    /// </remarks>
    public interface IStatisticsService
    {
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
    }
}