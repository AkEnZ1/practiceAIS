using System;
namespace DomainModel
{
    /// <summary>
    /// Типы должностей сотрудников
    /// </summary>
    public enum VacancyType
    {
        /// <summary>Руководитель</summary>
        Head,
        /// <summary>Стажер</summary>
        Intern,
        /// <summary>Менеджер</summary>
        Manager
    }
    /// <summary>
    /// Класс, представляющий сотрудника
    /// </summary>
    public class Employee : IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор сотрудника
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Опыт работы в годах
        /// </summary>
        public int WorkExp { get; set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public VacancyType Vacancy { get; set; }

        /// <summary>
        /// Возвращает строковое представление сотрудника
        /// </summary>
        /// <returns>Строка с информацией о сотруднике</returns>
        public override string ToString()
        {
            return $"ID: {ID}, Имя: {Name}, Должность: {Vacancy}, Опыт: {WorkExp} лет";
        }
    }
}