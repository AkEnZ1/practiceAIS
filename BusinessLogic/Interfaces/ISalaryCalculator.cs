using DomainModel;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс калькулятора зарплат
    /// </summary>
    /// <remarks>
    /// Определяет контракт для расчета заработной платы сотрудников.
    /// Реализует принцип единственной ответственности (SRP).
    /// </remarks>
    public interface ISalaryCalculator
    {
        /// <summary>
        /// Рассчитывает зарплату сотрудника на основе должности и опыта работы
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Рассчитанная зарплата</returns>
        double CalculateSalary(Employee employee);
    }
}