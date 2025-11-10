using BusinessLogic.Interfaces;
using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Сервис для получения статистики и аналитики по сотрудникам
    /// </summary>
    /// <remarks>
    /// Реализует бизнес-логику для генерации отчетов и статистических данных.
    /// Следует принципу единственной ответственности - отвечает только за статистические операции.
    /// </remarks>
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<Employee> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр StatisticsService
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным сотрудников</param>
        public StatisticsService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает общее количество сотрудников в системе
        /// </summary>
        /// <returns>Количество сотрудников</returns>
        public int GetTotalEmployees()
        {
            return _repository.GetAll().Count();
        }

        /// <summary>
        /// Рассчитывает средний опыт работы всех сотрудников
        /// </summary>
        /// <returns>Средний опыт работы в годах</returns>
        public double GetAverageExperience()
        {
            var employees = _repository.GetAll().ToList();
            if (!employees.Any()) return 0;

            return employees.Average(e => e.WorkExp);
        }

        /// <summary>
        /// Получает распределение сотрудников по должностям
        /// </summary>
        /// <returns>Словарь где ключ - должность, значение - количество сотрудников</returns>
        public Dictionary<VacancyType, int> GetVacancyDistribution()
        {
            return _repository.GetAll()
                .GroupBy(e => e.Vacancy)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Находит сотрудника с наибольшим опытом работы
        /// </summary>
        /// <returns>Сотрудник с максимальным опытом работы или null если сотрудников нет</returns>
        public Employee GetMostExperiencedEmployee()
        {
            var employees = _repository.GetAll().ToList();
            if (!employees.Any()) return null;

            return employees.OrderByDescending(e => e.WorkExp).First();
        }
    }
}