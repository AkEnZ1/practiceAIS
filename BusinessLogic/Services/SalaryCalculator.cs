using BusinessLogic.Interfaces;
using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Калькулятор для расчета зарплат сотрудников
    /// </summary>
    /// <remarks>
    /// Реализует бизнес-логику расчета заработной платы.
    /// Использует коэффициенты для разных должностей и базовую ставку.
    /// </remarks>
    public class SalaryCalculator : ISalaryCalculator
    {
        private readonly Dictionary<VacancyType, double> _multipliers = new Dictionary<VacancyType, double>()
        {
            { VacancyType.Head, 1.5 },
            { VacancyType.Manager, 1.25 },
            { VacancyType.Intern, 1.1 }
        };

        /// <summary>
        /// Рассчитывает зарплату сотрудника на основе должности и опыта работы
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Рассчитанная зарплата</returns>
        public double CalculateSalary(Employee employee)
        {
            if (_multipliers.TryGetValue(employee.Vacancy, out double multiplier))
                return employee.WorkExp * multiplier * 10000;

            return employee.WorkExp * 10000;
        }
    }
}