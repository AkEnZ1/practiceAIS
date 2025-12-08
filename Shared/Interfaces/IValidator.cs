using System.Collections.Generic;

namespace Shared.Interfaces
{
    /// <summary>
    /// Интерфейс для валидации данных
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Проверяет валидность имени сотрудника
        /// </summary>
        bool ValidateName(string name, out string error);

        /// <summary>
        /// Проверяет валидность опыта работы
        /// </summary>
        bool ValidateWorkExperience(int workExp, out string error);

        /// <summary>
        /// Проверяет валидность индекса сотрудника
        /// </summary>
        bool ValidateEmployeeIndex(int index, int totalEmployees, out string error);

        /// <summary>
        /// Проверяет валидность всех данных сотрудника
        /// </summary>
        bool ValidateEmployeeData(string name, int workExp, out List<string> errors);
    }
}